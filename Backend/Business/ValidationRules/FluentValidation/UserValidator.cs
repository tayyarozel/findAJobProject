using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Ad boş bırakılamaz");
            RuleFor(u => u.FirstName).Length(2,250).WithMessage("Ad uzunluğu min. 2, max.250 karakter olmalıdır");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("Soyad boş bırakılamaz");
            RuleFor(u => u.LastName).Length(2, 250).WithMessage("Soyad uzunluğu min. 2, max.250 karakter olmalıdır");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email adresi boş bırakılamaz");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçersiz Email adresi girdiniz");
          
            RuleFor(u => u.Title).MaximumLength(150).WithMessage("Başlık uzunluğu max 150 karakter olmalıdır").When(u => !string.IsNullOrEmpty(u.Title));
            RuleFor(u => u.Phone).Length(11).Must(StartWith).WithMessage("Geçersiz telefon numarası girdiniz").When(u => !string.IsNullOrEmpty(u.Phone));
        }

        private bool StartWith(string arg)
        {
            return arg.StartsWith("05");
        }
    }
   
}
