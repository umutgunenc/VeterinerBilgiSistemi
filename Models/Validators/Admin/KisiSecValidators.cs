using FluentValidation;
using System.Collections.Generic;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Data;
using System;
using System.Linq;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public partial class KisiSecValidators : AbstractValidator<KisiSecViewModel>
    {
        public KisiSecValidators()
        {
                RuleFor(x => x.InsanTckn)
                    .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                    .NotNull().WithMessage("Lütfen TCKN giriniz.")
                    .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                    .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                    .Must(FunctionsValidator.BeUsedTCKN).WithMessage("Girilen TCKN sistemde bulunamadı.")
                    .Must(FunctionsValidator.BeValidTCKN).WithMessage("Geçerli bir TCKN giriniz.");

        }

    }
}
