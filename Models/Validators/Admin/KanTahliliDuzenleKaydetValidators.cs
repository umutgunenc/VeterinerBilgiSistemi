using FluentValidation;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class KanTahliliDuzenleKaydetValidators : AbstractValidator<KanTahlilleriniGetirViewModel>
    {
        public KanTahliliDuzenleKaydetValidators()
        {
            RuleFor(x => x.KanTestiAdi)
                .NotEmpty().WithMessage("Kan tahlininin ismi boş olamaz")
                .NotNull().WithMessage("Kan tahlininin ismi boş olamaz")
                .MaximumLength(100).WithMessage("Maksimum 100 karakter uzunluğunda kan testi tanımlanabilir")
                .Must((model, KanTestiAdi) => FunctionsValidator.BeUniqueKanTathlilAdi(model.KanDegerleriId, KanTestiAdi)).WithMessage("Sistemde aynı isimde bir kan tahlili bulunmaktadır.");

            RuleFor(x => x.KanTestiBirimi)
                .NotEmpty().WithMessage("Tanımlanacak kan testinin adını giriniz.")
                .NotNull().WithMessage("Tanımlanacak kan testinin adını giriniz.")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda kan testi tanımlanabilir");

            RuleFor(x => x.AltLimit)
                .GreaterThanOrEqualTo(0).When(x => x.AltLimit.HasValue)
                .WithMessage("Alt limit sıfırdan küçük olamaz.");

            RuleFor(x => x.UstLimit)
                .GreaterThanOrEqualTo(0).When(x => x.UstLimit.HasValue)
                .WithMessage("Üst limit sıfırdan küçük olamaz.");

            RuleFor(x => x)
                .Must(x => x.AltLimit < x.UstLimit).When(x => x.AltLimit.HasValue && x.UstLimit.HasValue)
                .WithMessage("Alt limit, üst limitten büyük olamaz.");

            RuleFor(x => x.AktifMi)
                .Must(x => x == true || x == false).WithMessage("Lütfen Evet veya Hayır seçeneğini işaretleyiniz.");

        }
    }
}
