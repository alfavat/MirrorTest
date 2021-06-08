using Entity.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        bool ValidateToken(string token);
    }
}
