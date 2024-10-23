using FluentValidation;
using System;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class StokGirisKaydetValidators : AbstractValidator<StokGirisKaydetViewModel>
    {
        public StokGirisKaydetValidators()
        {

            RuleFor(x => x.StokId)
                .NotEmpty().WithMessage("Lütfen bir stok seçiniz.")
                .NotNull().WithMessage("Lütfen bir stok seçiniz.");

            RuleFor(x => x.AlisTarihi)
                .NotNull().WithMessage("Lütfen alış tarihini giriniz.")
                .NotEmpty().WithMessage("Lütfen alış tarihini giriniz.")
                .Must(x => x.HasValue && x.Value <= DateTime.Now).WithMessage("Alış tarihi, bugünden büyük olamaz.");

            RuleFor(x=>x.StokGirisAdet)
                .NotNull().WithMessage("Lütfen stok giriş adedini giriniz.")
                .NotEmpty().WithMessage("Lütfen stok giriş adedini giriniz.")
                .Must(x => x.HasValue && x.Value > 0).WithMessage("Giriş adedi 0'dan büyük olmalıdır.");
        }
    }
}
