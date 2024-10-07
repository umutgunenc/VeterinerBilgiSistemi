using FluentValidation;
using System.Security.Cryptography.Xml;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class KategoriEkleValidators : AbstractValidator<KategoriViewModel>
    {
        public KategoriEkleValidators()
        {
            RuleFor(x => x.KategoriAdi)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .NotNull().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeUniqueKategori).WithMessage("Bu kategori daha önceden sisteme eklenmiş.");

            RuleFor(x => x.IlacMi)
                .NotNull().WithMessage("Lütfen seçim yapınız.")
                .Must(x => x == true || x == false).WithMessage("Lütfen seçim yapınız.");
        }
    }
}
