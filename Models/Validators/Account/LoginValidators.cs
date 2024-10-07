using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Account;


namespace VeterinerBilgiSistemi.Models.Validators.Account
{
    public class LoginValidators : AbstractValidator<LoginViewModel>
    {
        public LoginValidators()
        {

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .NotNull().WithMessage("Kullanıcı adı boş olamaz.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Şifre boş olamaz.");

            RuleFor(x => x.PasswordHash)
                .Must((user, sifre) => FunctionsValidator.BeValidPasswordDate(user.UserName, sifre))
                .When((user) => FunctionsValidator.LoginSucceed(user))
                .WithMessage("Şifre geçerlilik süresi dolmuş. Lütfen şifrenizi değiştiriniz.");


        }

    }
}
