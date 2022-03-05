using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager:IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        //kullanıcı kayıt olma operasyonu
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            var result = regex.IsMatch(userForRegisterDto.Password);
            if (!result)
            {
                return new ErrorDataResult<User>(Messages.UserPasswordWrong);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash,out passwordSalt);// gelen şifre ile haş oluşturduk
            var user = new User
            {
                Email = userForRegisterDto.Email,
               
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            }; // user nesnesi oluşturduk
            _userService.Add(user); //ve ekledik

            return new SuccessDataResult<User>(user,Messages.UserRegistered);
        }

        // kullanıcı login olma
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);// mail adresine göre kullanıcı geit
            if (userToCheck==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);// kullanıcı bulunamadı
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.Data.PasswordHash,userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);//şifre hatalı
            }

            return new SuccessDataResult<User>(userToCheck.Data,Messages.SuccessfulLogin);//lgin oldu
        }

        //kullanıcı varmı kontrolu
        public IResult UserExists(string email)
        {
            
            if (_userService.GetByMail(email).Data!=null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)

        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }
    }
}
