using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurement.SharedKernel.Repository;

namespace QuantityMeasurement.SharedKernel.Auth
{
    /// <summary>
    /// Handles credential verification and signed JWT generation.
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUserCredentialRepository _userRepo;

        public JwtTokenService(IOptions<JwtOptions> jwtOptions, IUserCredentialRepository userRepo)
        {
            _jwtOptions = jwtOptions.Value;
            _userRepo = userRepo;
        }

        public bool ValidateCredentials(string username, string password, out string role)
        {
            role = "User";
            var user = _userRepo.GetByUsername(username);
            if (user == null) return false;
            role = user.Role;
            return PasswordHasher.Verify(password, user.PasswordHash);
        }

        public string GenerateToken(string username, string role)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Name, username),
                new(ClaimTypes.NameIdentifier, username),
                new(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
