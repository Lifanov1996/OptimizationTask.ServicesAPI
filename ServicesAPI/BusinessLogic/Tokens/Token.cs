using Microsoft.IdentityModel.Tokens;
using ServicesAPI.BusinessLogic.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServicesAPI.BusinessLogic.Tokens
{
    public class Token : IToken
    {
        private readonly IConfiguration _configuration;
        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                //expires: DateTime.Now.AddHours(3),
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
