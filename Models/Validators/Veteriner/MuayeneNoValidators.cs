using FluentValidation;
using System;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Veteriner;

namespace VeterinerBilgiSistemi.Models.Validators.Veteriner
{
    public class MuayeneNoValidators :AbstractValidator<MuayeneNoViewModel>
    {
        public MuayeneNoValidators()
        {
            RuleFor(x => x.Muayene.SonrakiMuayeneTarihi)
                .NotEmpty().WithMessage("Lütfen sonraki muayene tarihini giriniz.")
                .NotNull().WithMessage("Lütfen sonraki muayene tarihini giriniz.")
                .Must(x => x.HasValue && x.Value.Date >= DateTime.Now.Date)
                .WithMessage("Sonraki muayene tarihi, bugunden önce olamaz.");

            RuleForEach(x => x.YapilanKanTestleri)
                .Must(kanTesti => !kanTesti.KanDegeriValue.HasValue || kanTesti.SecildiMi)
                .WithName("YapilanKanTestleri[{CollectionIndex}]")
                .WithMessage("Lütfen seçim yapınız.");

            RuleForEach(x => x.YapilanKanTestleri)
                .Must(kanTesti => !kanTesti.SecildiMi || kanTesti.KanDegeriValue.HasValue)
                .WithName("YapilanKanTestleri[{CollectionIndex}]")
                .WithMessage("Seçim yapıldı ancak test sonucu girilmedi.");

            RuleForEach(x => x.YapilanKanTestleri)
                .Must(kanTesti => !kanTesti.SecildiMi || (kanTesti.KanDegeriValue.HasValue && kanTesti.KanDegeriValue > 0))
                .WithName("YapilanKanTestleri[{CollectionIndex}]")
                .WithMessage("Kan değeri pozitif bir değer olmalıdır.");

            RuleForEach(x => x.KullanilanIlaclar)
                .Must(stok => !stok.StokCikisAdet.HasValue || stok.YapildiMi)
                .WithName("KullanilanIlaclar[{CollectionIndex}]")
                .WithMessage("Lütfen seçim yapınız.");

            RuleForEach(x => x.KullanilanIlaclar)
                .Must(stok => !stok.YapildiMi || stok.StokCikisAdet.HasValue)
                .WithName("KullanilanIlaclar[{CollectionIndex}]")
                .WithMessage("Seçim yapıldı ancak miktar girilmedi.");

            RuleForEach(x => x.KullanilanIlaclar)
                .Must(stok => !stok.YapildiMi || (stok.StokCikisAdet.HasValue && stok.StokCikisAdet > 0))
                .WithName("KullanilanIlaclar[{CollectionIndex}]")
                .WithMessage("Stok miktarı pozitif bir değer olmalıdır.");

            RuleForEach(x => x.KullanilanIlaclar)
                .Must((model, stok) =>
                    (stok.StokCikisAdet.HasValue && stok.StokCikisAdet > 0 && stok.YapildiMi) ?
                    FunctionsValidator.BePositiveStock(stok.Stok.Id, stok.StokCikisAdet.Value) : true
                )
                .WithName("KullanilanIlaclar[{CollectionIndex}]")
                .WithMessage("Girilen miktara göre stok miktarı negatif olmaktadır.\nGirilen miktarı veya deponuzda bulunan stok sayısını kontrol ediniz.");

            RuleFor(x => x.Muayene.HastalikId)
                .NotNull().WithMessage("Lütfen hayvanın sağlık durumunu giriniz.")
                .NotEmpty().WithMessage("Lütfen hayvanın sağlık durumunu giriniz.")
                .Must(FunctionsValidator.BeHastalik).WithMessage("Seçilen hastalık sistemde tanımlı değil.");
        }
    }
}
