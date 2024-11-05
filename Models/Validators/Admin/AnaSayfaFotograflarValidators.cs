using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class AnaSayfaFotograflarValidators : AbstractValidator<AnaSayfaFotografEkleViewModel>
    {
        public AnaSayfaFotograflarValidators()
        {
            RuleFor(x => x.FotografAdi)
                .NotEmpty().WithMessage("Lütfen yüklecek fotograf için bir isim giriniz.")
                .NotNull().WithMessage("Lütfen yüklecek fotograf için bir isim giriniz.")
                .Must(FunctionsValidator.BeUniqueAnaSayfaFotografAdi).WithMessage("Girilen isimde bir fotoğraf sistemde kayıtlı");

            RuleFor(x => x.FilePhoto)
                .NotEmpty().WithMessage("Lütfen bir fotograf seçiniz.")
                .NotNull().WithMessage("Lütfen bir fotoğraf seçiniz.")
                .Must(FunctionsValidator.BeValidExtensionForPhoto).WithMessage("Yalnızca jpg, jpeg, png ve bmp uzantılı dosyalar yüklenebilir.");

            RuleFor(x => x.FilePhoto)
                .Must(x => x.Length < 10485760)
                .When(x => x.FilePhoto != null)
                .WithMessage("Fotoğraf boyutu 10MB'dan küçük olmalıdır.");


        }
    }
}
