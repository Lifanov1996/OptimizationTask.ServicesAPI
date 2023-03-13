using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ServicesAPI.BusinessLogic.Contracts
{
    public interface IToken
    {
        /// <summary>
        /// Генерация токена
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns></returns>
        JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}
