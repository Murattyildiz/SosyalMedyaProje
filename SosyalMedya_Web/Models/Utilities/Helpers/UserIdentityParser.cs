using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SosyalMedya_Web.Models.Utilities.Helpers
{
    public class UserIdentityParser
    {
        public static int GetUserIdentityFromJwtToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;
            int userId = Convert.ToInt32(jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            return userId;
        }
    }
}
