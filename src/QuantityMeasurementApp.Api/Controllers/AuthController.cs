using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Google.Apis.Auth;
using QuantityMeasurementApp.Repository;
using QuantityMeasurementApp.Authentication;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Repository.Models;

namespace QuantityMeasurementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly JwtOptions _jwtOptions;
        private readonly IUserCredentialRepository _userCredentialRepository;

        public AuthController(
            IJwtTokenService jwtTokenService,
            IOptions<JwtOptions> jwtOptions,
            IUserCredentialRepository userCredentialRepository)
        {
            _jwtTokenService = jwtTokenService;
            _jwtOptions = jwtOptions.Value;
            _userCredentialRepository = userCredentialRepository;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<LoginResponseDto> Register([FromBody] RegisterUserRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest(new { message = "Username is required" });
            }

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
            {
                return BadRequest(new { message = "Password must be at least 6 characters" });
            }

            if (_userCredentialRepository.Exists(request.Username))
            {
                return Conflict(new { message = "Username already exists" });
            }

            var normalizedRole = string.Equals(request.Role, "Admin", StringComparison.OrdinalIgnoreCase)
                ? "Admin"
                : "User";

            _userCredentialRepository.Add(new UserCredentialRecord
            {
                Username = request.Username,
                PasswordHash = QuantityMeasurementApp.Authentication.PasswordHasher.Hash(request.Password),
                Role = normalizedRole,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            });

            var token = _jwtTokenService.GenerateToken(request.Username, normalizedRole);
            var expiry = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Username = request.Username,
                ExpiresAtUtc = expiry
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto request)
        {
            if (!_jwtTokenService.ValidateCredentials(request.Username, request.Password, out var role))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var token = _jwtTokenService.GenerateToken(request.Username, role);
            var expiry = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Username = request.Username,
                ExpiresAtUtc = expiry
            });
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<ActionResult<LoginResponseDto>> LoginWithGoogle([FromBody] GoogleAuthRequestDto request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.IdToken))
            {
                return BadRequest(new { message = "Google idToken is required" });
            }

            var googleClientId = HttpContext.RequestServices
                .GetRequiredService<IConfiguration>()
                .GetSection("GoogleAuth")
                .GetValue<string>("ClientId");

            if (string.IsNullOrWhiteSpace(googleClientId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Google auth is not configured on server" });
            }

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { googleClientId }
                });
            }
            catch
            {
                return Unauthorized(new { message = "Invalid Google token" });
            }

            if (string.IsNullOrWhiteSpace(payload.Email))
            {
                return Unauthorized(new { message = "Google account email not available" });
            }

            var username = payload.Email.Trim();
            var user = _userCredentialRepository.GetByUsername(username);

            if (user is null && !_userCredentialRepository.Exists(username))
            {
                user = new UserCredentialRecord
                {
                    Username = username,
                    PasswordHash = QuantityMeasurementApp.Authentication.PasswordHasher.Hash(Guid.NewGuid().ToString("N")),
                    Role = "User",
                    IsActive = true,
                    CreatedAtUtc = DateTime.UtcNow
                };
                _userCredentialRepository.Add(user);
            }

            if (user is null)
            {
                return Unauthorized(new { message = "User is inactive" });
            }

            var token = _jwtTokenService.GenerateToken(user.Username, user.Role);
            var expiry = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Username = user.Username,
                ExpiresAtUtc = expiry
            });
        }
    }
}




