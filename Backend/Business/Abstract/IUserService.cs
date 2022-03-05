using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {

        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        //kullanıcı roller getir
        IDataResult<List<OperationClaim>> GetClaims(User user);
        //kullanıcı ekle
        IResult Add(User user);
        // kullanıcı maile göre getir
        IDataResult<User> GetByMail(string email);

        IResult Update(User user);
        IResult Delete(User user);
        IResult AvatarAdd(UserForAvatarUploadDto userForAvatarUploadDto);
        IResult AvatarDelete(UserForAvatarUploadDto userForAvatarUploadDto);
        IResult AvatarUpdate(UserForAvatarUploadDto userForAvatarUploadDto);

        IResult PasswordUpdate(UserForPasswordUpdateDto userForPasswordUpdateDto);
    }
}
