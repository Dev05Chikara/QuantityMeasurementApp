using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Google.Apis.Auth;
using QuantityMeasurement.SharedKernel.Auth;
using QuantityMeasurement.SharedKernel.DTOs;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.AuthService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly JwtOptions _jwtOptions;
        private readonly IUserCredentialRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IJwtTokenService jwtTokenService,
            IOptions<JwtOptions> jwtOptions,
            IUserCredentialRepository userRepo,
            IConfiguration config,
            ILogger<AuthController> logger)
        {
            _jwtTokenService = jwtTokenService;
            _jwtOptions = jwtOptions.Value;
            _userRepo = userRepo;
            _config = config;
            _logger = logger;
        }

        /// <summary>Register a new user account.</summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<LoginResponseDto> Register([FromBody] RegisterUserRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username))
                    return BadRequest(new { message = "Username is required" });

                if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                    return BadRequest(new { message = "Password must be at least 6 characters" });

                if (_userRepo.Exists(request.Username))
                    return Conflict(new { message = "Username already exists" });

                var role = string.Equals(request.Role, "Admin", StringComparison.OrdinalIgnoreCase) ? "Admin" : "User";

                _userRepo.Add(new UserCredentialRecord
                {
                    Username     = request.Username,
                    PasswordHash = PasswordHasher.Hash(request.Password),
                    Role         = role,
                    IsActive     = true,
                    CreatedAtUtc = DateTime.UtcNow
                });

                var token = _jwtTokenService.GenerateToken(request.Username, role);
                return Ok(new LoginResponseDto
                {
                    Token        = token,
                    Username     = request.Username,
                    ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register failed for user '{Username}'", request.Username);
                return StatusCode(503, new
                {
                    message = "Cannot reach the database. If using Azure SQL, add your IP to the SQL Server firewall in Azure Portal.",
                    detail  = ex.Message
                });
            }
        }

        /// <summary>Login with username and password.</summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                if (!_jwtTokenService.ValidateCredentials(request.Username, request.Password, out var role))
                    return Unauthorized(new { message = "Invalid username or password" });

                var token = _jwtTokenService.GenerateToken(request.Username, role);
                return Ok(new LoginResponseDto
                {
                    Token        = token,
                    Username     = request.Username,
                    ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for user '{Username}'", request.Username);
                return StatusCode(503, new
                {
                    message = "Cannot reach the database. If using Azure SQL, add your IP to the SQL Server firewall in Azure Portal.",
                    detail  = ex.Message
                });
            }
        }

        /// <summary>Login with a Google OAuth ID token.</summary>
        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<ActionResult<LoginResponseDto>> LoginWithGoogle([FromBody] GoogleAuthRequestDto request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.IdToken))
                return BadRequest(new { message = "Google idToken is required" });

            var googleClientId = _config.GetSection("GoogleAuth").GetValue<string>("ClientId");
            if (string.IsNullOrWhiteSpace(googleClientId))
                return StatusCode(500, new { message = "Google auth is not configured on server" });

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken,
                    new GoogleJsonWebSignature.ValidationSettings { Audience = new[] { googleClientId } });
            }
            catch
            {
                return Unauthorized(new { message = "Invalid Google token" });
            }

            if (string.IsNullOrWhiteSpace(payload.Email))
                return Unauthorized(new { message = "Google account email not available" });

            try
            {
                var username = payload.Email.Trim();
                var user = _userRepo.GetByUsername(username);

                if (user is null && !_userRepo.Exists(username))
                {
                    user = new UserCredentialRecord
                    {
                        Username     = username,
                        PasswordHash = PasswordHasher.Hash(Guid.NewGuid().ToString("N")),
                        Role         = "User",
                        IsActive     = true,
                        CreatedAtUtc = DateTime.UtcNow
                    };
                    _userRepo.Add(user);
                }

                if (user is null)
                    return Unauthorized(new { message = "User is inactive" });

                var token = _jwtTokenService.GenerateToken(user.Username, user.Role);
                return Ok(new LoginResponseDto
                {
                    Token        = token,
                    Username     = user.Username,
                    ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Google login failed for '{Email}'", payload.Email);
                return StatusCode(503, new
                {
                    message = "Cannot reach the database. If using Azure SQL, add your IP to the SQL Server firewall in Azure Portal.",
                    detail  = ex.Message
                });
            }
        }
    }
}
