﻿using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Account;

namespace VeterinerBilgiSistemi.Models.Validators.Account
{
    public class ChangePaswordValidators : AbstractValidator<ChangePaswordViewModel>
    {

        public ChangePaswordValidators()
        {
            //TODO yeni şifre için validasyonlar ekle


            //controller icinden kontrol ediliyor
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Eski şifre alanı boş geçilemez")
                .NotNull().WithMessage("Eski şifre alanı boş geçilemez");
            //.MustAsync(async (model, oldPassword, cancellationToken) =>
            //await FunctionsValidator.BeCorrectOldPasswordAsync(oldPassword, model.UserName, _userManager, cancellationToken))
            //.WithMessage("Lütfen şifrenizi doğru giriniz.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre alanı boş geçilemez")
                .NotNull().WithMessage("Yeni şifre alanı boş geçilemez")
                .NotEqual(x => x.OldPassword).WithMessage("Yeni şifreniz, eski şifreniz ile aynı olamaz.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Şifre maksimum 50 karakter olabilir.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Sifre tekrar alanı boş geçilemez")
                .NotNull().WithMessage("Sifre tekrar alanı boş geçilemez")
                .Equal(x => x.NewPassword).WithMessage("Şifreler uyuşmuyor")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Şifre maksimum 50 karakter olabilir."); ;
        }

        //private async Task<bool> bePassword(string oldPassword)
        //{
        //    var user = await _userManager.GetUserAsync(_user);
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
        //    return result == PasswordVerificationResult.Success;
        //}
    }
}
