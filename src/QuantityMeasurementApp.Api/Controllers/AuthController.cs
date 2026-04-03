using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public IActionResult Register([FromBody] RegisterUserRequestDto request)
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

            return Ok(new { message = "User registered successfully" });
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
                ExpiresAtUtc = expiry
            });
        }
    }
}




