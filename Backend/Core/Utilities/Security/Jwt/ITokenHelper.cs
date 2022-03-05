using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    //Token üretecek mekanizma
    public interface ITokenHelper
    {
        //İlgili kullanıcı için ilgili OperationClaimleri içerecek bir token üret
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
