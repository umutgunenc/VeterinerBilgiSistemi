using FluentValidation;
using System;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class StokCikisKaydetValidators : AbstractValidator<StokCikisKaydetViewModel>
    {
        public StokCikisKaydetValidators()
        {

            RuleFor(x => x.StokId)
                .NotEmpty().WithMessage("Lütfen bir stok seçiniz.")
                .NotNull().WithMessage("Lütfen bir stok seçiniz.");        

            RuleFor(x => x.SatisTarihi)
                .NotNull().WithMessage("Lütfen stok çıkış tarihini giriniz.")
                .NotEmpty().WithMessage("Lütfen stok çıkış tarihini giriniz.")
                .Must(x => x.HasValue && x.Value <= DateTime.Now).WithMessage("Satış tarihi, bugünden büyük olamaz.");

            RuleFor(x=>x.StokCikisAdet)
                .NotNull().WithMessage("Lütfen stok çıkış adedini giriniz.")
                .NotEmpty().WithMessage("Lütfen stok çıkış adedini giriniz.")
                .Must(x => x.HasValue && x.Value > 0).WithMessage("Stok çıkış miktarı 0'dan büyük olmalıdır.")
                .Must((model,adet)=>FunctionsValidator.BePositiveStock(model,adet)).WithMessage("Aktif stok sayısı negatif olacaktır. Stok miktarlarını kontrol ediniz.");
            
        }
    }
}
