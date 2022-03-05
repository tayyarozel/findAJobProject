using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidator:AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
           
        }

      
    }
}
