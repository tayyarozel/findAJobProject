using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            // gönderilen validator type bir IValidator değilse hata ver
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }

        // bu method MethodInterception'dan geliyor.  BU işlem nerede isterse orada calıssın kodu
        protected override void OnBefore(IInvocation invocation)
        {
            //Activator.CreateInstance: normalde birseyı kullanmadan önce newleriz ama bu tanım çalışma sırasında kullanmak newlemek için kullanılır
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //Gönderdiğim _validatorType'in (ProductValidator) base typeni bul yani (AbstractValidator) onunda generic argumanlarını bul yani <Product>
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
           // ve onun parametrelerini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            // bu parametreleri gez ve ValidationToolu kullanarak kontrol et
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator,entity);
            }
        }
    }
}
