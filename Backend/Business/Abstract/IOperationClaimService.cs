using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetAll();
        IDataResult<OperationClaim> GetById(int id);
        IResult Add(OperationClaim operationClaim);
        IDataResult<OperationClaim> GetByName(string name);

        IResult Update(OperationClaim operationClaim);
        IResult Delete(OperationClaim operationClaim);
        
    }
}
