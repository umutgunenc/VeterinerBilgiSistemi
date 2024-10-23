using FluentValidation;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class KanTahliliTanimlaValidators : AbstractValidator<KanTahliliTanimlaViewModel>
    {
        public KanTahliliTanimlaValidators()
        {
            RuleFor(x => x.KanTestiAdi)
                .NotEmpty().WithMessage("Tanımlanacak kan testinin adını giriniz.")
                .NotNull().WithMessage("Tanımlanacak kan testinin adını giriniz.")
                .MaximumLength(100).WithMessage("Maksimum 100 karakter uzunluğunda kan testi tanımlanabilir")
                .Must(ValidateFunctions.FunctionsValidator.BeUniqueKanTathlilAdi).WithMessage("Bu kan tahlili sisteme kayıtlı.");

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

            RuleFor(x => x.AltLimit)
                .Must((model, x) => x < model.UstLimit)
                .When(x => x.AltLimit.HasValue && x.UstLimit.HasValue).WithMessage("Alt limit, üst limitten büyük olamaz.");

            RuleFor(x => x.UstLimit)
                .Must((model, x) => x > model.AltLimit)
                .When(x => x.AltLimit.HasValue && x.UstLimit.HasValue).WithMessage("Üst limit, alt limitten küçük olamaz.");


        }
    }
}
