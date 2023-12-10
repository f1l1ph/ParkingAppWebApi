using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParkingAppWebApi.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(config["TokenKey"] ?? string.Empty));

        public string CreateToken(string userName)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, userName)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
