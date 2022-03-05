using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger))]
    [ExceptionLogAspect(typeof(DatabaseLogger))]
    public class UserManager:IUserService
    {
        IUserDal _userDal;
        IFileHelper _fileHelper;

        public UserManager(IUserDal userDal, IFileHelper fileHelper)
        {
            _userDal = userDal;
            _fileHelper =fileHelper;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            IResult result = BusinessRules.Run(CheckIfEmailToAdd(user.Email));
            if (result != null)
            {
                return result;
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }


        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }



        [SecuredOperation("User")]
        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            IResult result = BusinessRules.Run(CheckIfUser(user.Id), CheckIfEmailToUpdate(user));
            if (result != null)
            {
                return result;
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            IResult result = BusinessRules.Run(CheckIfUser(user.Id));
            if (result != null)
            {
                return result;
            }
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        [CacheAspect()]
        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetList());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }


        [SecuredOperation("User")]
        public IResult AvatarAdd(UserForAvatarUploadDto userForAvatarUploadDto)
        {
            IResult result = BusinessRules.Run(CheckIfImage(userForAvatarUploadDto.Image),CheckIfUser(userForAvatarUploadDto.UserId));
            if (result != null)
            {
                return result;
            }
            var user = _userDal.Get(u => u.Id == userForAvatarUploadDto.UserId);
            var avatar = _fileHelper.Upload(userForAvatarUploadDto.Image, Paths.UserAvatarPath);
            if (avatar == null)
            {
                return new ErrorResult(Messages.UserAvatarNotAdded);
            }
            user.Avatar = avatar;
            _userDal.Update(user);
            return new SuccessResult(Messages.UserAvatarAdded);
        }


        [SecuredOperation("User")]
        public IResult AvatarDelete(UserForAvatarUploadDto userForAvatarUploadDto)
        {
            IResult result = BusinessRules.Run(CheckIfUser(userForAvatarUploadDto.UserId), CheckIfUserAvatar(userForAvatarUploadDto.UserId));
            if (result != null)
            {
                return result;
            }
            var user = _userDal.Get(u => u.Id == userForAvatarUploadDto.UserId);
            _fileHelper.Delete(user.Avatar);
            user.Avatar = null;
            _userDal.Update(user);
            return new SuccessResult(Messages.UserAvatarDeleted);
        }

        

        [SecuredOperation("User")]
        public IResult AvatarUpdate(UserForAvatarUploadDto userForAvatarUploadDto)
        {
            IResult result = BusinessRules.Run(CheckIfUser(userForAvatarUploadDto.UserId));
            if (result != null)
            {
                return result;
            }
            var user = _userDal.Get(u => u.Id == userForAvatarUploadDto.UserId);
            var newAvatar=_fileHelper.Update(userForAvatarUploadDto.Image, user.Avatar,Paths.UserAvatarPath);
            if (newAvatar == null)
            {
                return new ErrorResult(Messages.UserAvatarNotUpdated);
            }
            user.Avatar = newAvatar;
            _userDal.Update(user);
            return new SuccessResult(Messages.UserAvatarUpdated);
        }


        

        public IResult PasswordUpdate(UserForPasswordUpdateDto userForPasswordUpdateDto)
        {
           
            byte[] passwordHash, passwordSalt;
            var userToCheck = _userDal.Get(u => u.Id == userForPasswordUpdateDto.UserId);
            if (userToCheck==null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            IResult result = BusinessRules.Run(CheckIfUser(userForPasswordUpdateDto.UserId)
                ,CheckIfNewPassword(userForPasswordUpdateDto.NewPassword, userForPasswordUpdateDto.NewPasswordRepeat)
                ,CheckIfActivePassword(userForPasswordUpdateDto.ActivePassword, userToCheck.PasswordHash, userToCheck.PasswordSalt));
            if (result != null)
            {
                return result;
            }

            HashingHelper.CreatePasswordHash(userForPasswordUpdateDto.NewPassword, out passwordHash, out passwordSalt);
            userToCheck.PasswordHash = passwordHash;
            userToCheck.PasswordSalt = passwordSalt;
            _userDal.Update(userToCheck);
            return new SuccessResult(Messages.UserPasswordUpdated);
        }




        private IResult CheckIfActivePassword(string activePassword, byte[] passwordHash, byte[] passwordSalt)
        {

            if (!HashingHelper.VerifyPasswordHash(activePassword, passwordHash, passwordSalt))
            {
                return new ErrorResult(Messages.UserActivePasswordWrong);
            }
            return new SuccessResult();
        }

        private IResult CheckIfNewPassword(string newPassword, string newPasswordRepeat)
        {
            if (newPassword != newPasswordRepeat)
            {
                return new ErrorResult(Messages.UserNewPasswordsNotMatch);
            }
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            var result = regex.IsMatch(newPassword);
            if (!result)
            {
                return new ErrorResult(Messages.UserNewPasswordWrong);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImage(IFormFile image)
        {
            if (image.Length <= 0 || image.Length > 2100000)
            {
                return new ErrorResult(Messages.UserAvatarSizeIncorrect);
            }


            var type = image.ContentType;
            if (type != "image/png" || type != "image/jpeg" || type != "image/png")
            {
                return new ErrorResult(Messages.UserAvatarExtensionIncorrect);
            }
            return new SuccessResult();
        }

        private IResult CheckIfUserAvatar(int id)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null || user.Avatar == null)
            {
                return new ErrorResult(Messages.UserAvatarNotDeleted);
            }
            return new SuccessResult();
        }



        private IResult CheckIfEmailToUpdate(User user)
        {
            var result = _userDal.Get(u => u.Id != user.Id && u.Email == user.Email);
            if (result != null)
            {
                return new ErrorResult(Messages.UserEmailAlreadyExists);
            }
            return new SuccessResult();
        }



        private IResult CheckIfUser(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            if (result == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }

        private IResult CheckIfEmailToAdd(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            if (result != null)
            {
                return new ErrorResult(Messages.UserEmailAlreadyExists);
            }
            return new SuccessResult();
        }


    }
}
