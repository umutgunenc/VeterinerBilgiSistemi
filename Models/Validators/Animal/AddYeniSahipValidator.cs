﻿using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Animal;

namespace VeterinerBilgiSistemi.Models.Validators.Animal
{
    public class AddYeniSahipValidator : AbstractValidator<AddNewSahipViewModel>
    {

        public AddYeniSahipValidator()
        {
            RuleFor(x => x.YeniSahipTCKN)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(FunctionsValidator.BeUsedTCKN).WithMessage("Girilen TCKN sistemde bulunamadı.")
                .Must(FunctionsValidator.BeValidTCKN).WithMessage("Geçerli bir TCKN giriniz.")
                .Must((model, yeniSahipTCKN) => FunctionsValidator.BeNotOwnedAnimal(model.HayvanId, yeniSahipTCKN)).WithMessage("Yeni sahip zaten bu hayvanının sahibi.");

        }
    }
}
