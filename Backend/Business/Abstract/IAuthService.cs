using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        //Kullancı kayıt olma
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        //kullanıcı üye olma
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        // kullanıcı varmı
        IResult UserExists(string email);
        //kullanıcı token üretme
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
