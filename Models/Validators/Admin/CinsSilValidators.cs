using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class CinsSilValidator : AbstractValidator<CinsSilViewModel>
    {

        public CinsSilValidator()
        {

            RuleFor(x => x.CinsId)
                .NotNull().WithMessage("Lütfen bir cins seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir cins seçiniz.")
                .Must(FunctionsValidator.BeCins).WithMessage("Listede olmayan bir cinsi silemezsiniz.")
                .Must(FunctionsValidator.BeNotMatchedCins).WithMessage("Silinecek cinse ait tanımlı tür olduğu için silme işlemi gerçekleştirilemedi.");
        }

    }
}