using FluentValidation;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class RenkSilValidator : AbstractValidator<RenkSilViewModel>
    {

        public RenkSilValidator()
        {

            RuleFor(x => x.RenkId)
                .NotNull().WithMessage("Lütfen bir renk seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir renk seçiniz.")
                .Must(FunctionsValidator.BeRenk).WithMessage("Listede olmayan bir rengi silemezsiniz.")
                .Must(FunctionsValidator.BeNotUsedRenk).WithMessage("Silinmek istenen renk bir hayvana atandığı için silme işlemi başarısız oldu.");

        }


    }
}