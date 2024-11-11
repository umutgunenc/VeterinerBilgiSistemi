using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Veteriner;

namespace VeterinerBilgiSistemi.Models.Validators.Veteriner
{
    public class KisiSecValidators :AbstractValidator<KisiSecVeterinerViewModel>
    {
        public KisiSecValidators()
        {
            RuleFor(x=>x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(FunctionsValidator.BeUsedTCKN).WithMessage("Girilen TCKN sistemde bulunamadı.")
                .Must(FunctionsValidator.BeValidTCKN).WithMessage("Geçerli bir TCKN giriniz.");
        }
    }
}
