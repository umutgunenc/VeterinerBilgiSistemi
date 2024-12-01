using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class KategoriSilValidator : AbstractValidator<KategoriSilViewModel>
    {
        public KategoriSilValidator()
        {
            RuleFor(x => x.KategoriId)
                .NotEmpty().WithMessage("Kategori seçimi yapmalısınız.")
                .NotNull().WithMessage("Kategori seçimi yapmalısınız.")
                .Must(FunctionsValidator.BeKategori).WithMessage("Seçilen kategori sistemde bulunamadı.")
                .Must(FunctionsValidator.BeNotMatchedKategori).WithMessage("Seçilen kategoride stok bulunduğu için silme işlemi başarısız oldu.");
        }
    }
}
