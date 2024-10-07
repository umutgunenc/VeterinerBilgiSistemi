using VeterinerBilgiSistemi.Data;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class RenkEkleValidators : AbstractValidator<RenkEkleViewModel>
    {

        public RenkEkleValidators()
        {

            RuleFor(x => x.RenkAdi)
                .NotNull().WithMessage("Lütfen bir renk giriniz.")
                .NotEmpty().WithMessage("Lütfen bir renk giriniz.")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda değer girilebilir.")
                .Must(FunctionsValidator.BeUniqueRenk).WithMessage("Girdiğiniz renk zaten sisteme kayıtlı.");
        }


    }
}

