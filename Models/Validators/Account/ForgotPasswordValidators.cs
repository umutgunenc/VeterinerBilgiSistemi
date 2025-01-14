using FluentValidation;
using System;
using System.Linq;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Account;

namespace VeterinerBilgiSistemi.Models.Validators.Account
{
    public class ForgotPasswordValidators : AbstractValidator<ForgotPasswordViewModel>
    {
        public ForgotPasswordValidators()
        {
            // Şifremi Unuttum Formu

            // TCKN'e ait kurallar
            RuleFor(x => x.InsanTckn)   
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(FunctionsValidator.BeValidTCKN).WithMessage("Geçerli bir TCKN giriniz.");

            //E mail adresine ait kurallar
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.");

            //Telefon numarasına ait kurallar
            RuleFor(x => x.PhoneNumber)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telefon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.");

        }


    }
}