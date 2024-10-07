using FluentValidation;
using System.Linq;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public partial class CinsEkleValidators : AbstractValidator<CinsEkleViewModel>
    {
        public CinsEkleValidators()
        {
            RuleFor(x => x.CinsAdi)
                .NotNull().WithMessage("Lütfen bir cins giriniz.")
                .NotEmpty().WithMessage("Lütfen bir cins giriniz.")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter kullanabilirsiniz.")
                .Must(FunctionsValidator.BeUniqueCins).WithMessage("Girdiğiniz cins zaten sisteme kayıtlı");
        }
    }
}
