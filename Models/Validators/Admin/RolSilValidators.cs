using FluentValidation;
using System.Linq;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public partial class RolSilValidators : AbstractValidator<RolSilViewModel>
    {

        public RolSilValidators()
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Silinecek rolü seçiniz.")
                .NotNull().WithMessage("Silinecek rolü seçiniz.")
                .Must(FunctionsValidator.BeRol).WithMessage("Silinecek rol sistemde bulunamadı.")
                .Must(FunctionsValidator.BeNotMatchedRol).WithMessage("Silinecek role tanımlı kişiler olduğu için silme işlemi gerçekleştirilemedi.");
        }


    }
}
