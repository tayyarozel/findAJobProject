using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        // kullanıc rollerini getirecek join operasyonu
        List<OperationClaim> GetClaims(User user);
    }
}
