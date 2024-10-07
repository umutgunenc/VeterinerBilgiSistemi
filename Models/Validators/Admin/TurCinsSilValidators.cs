using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public partial class TurCinsSilValidators : AbstractValidator<CinsTurEslesmeKaldirViewModel>
    {
        public TurCinsSilValidators()
        {

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Bir kayıt seçiniz.")
                .NotEmpty().WithMessage("Bir kayıt seçiniz.")
                .Must(FunctionsValidator.BeTurCins).WithMessage("Seçilen Kayıt sisteme kayıtlı olmalı.")
                .Must(FunctionsValidator.BeNotUsedTurCins).WithMessage("Seçilen tür ve cinse ait bir hayvan kaydı olduğu için silme işlemi başarısız oldu");
        }


    }
}
