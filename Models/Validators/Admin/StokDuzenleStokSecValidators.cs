using FluentValidation;
using System;
using System.Linq;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class StokDuzenleStokSecValidators : AbstractValidator<StokDuzenleStokSecViewModel>
    {
        public StokDuzenleStokSecValidators()
        {
            RuleFor(x => x.GirilenBarkodNo)
                .NotEmpty().WithMessage("Barkod numarası boş olamaz.")
                .NotNull().WithMessage("Barkod numarası boş olamaz.")
                .MaximumLength(50).WithMessage("Barkod Numarası maksimum 50 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeInStock).WithMessage("Girilen barkod numarası sistemde bulunamadı.");
        }
    }
}
