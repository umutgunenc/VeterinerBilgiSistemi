using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class StokCikisValidators :AbstractValidator<StokCikisViewModel>
    {
        public StokCikisValidators()
        {
            RuleFor(x=>x.ArananMetin)
                .NotEmpty().WithMessage("Aranan kelime boş olamaz")
                .NotNull().WithMessage("Aranan kelime boş olamaz")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda bir arama yapılabilir.")
                .Must(FunctionsValidator.SeacrhInStock).WithMessage("Aranan stok bulunamadı.");
        }
    }
}
