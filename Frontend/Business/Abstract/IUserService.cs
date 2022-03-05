using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results;

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
    }
}
