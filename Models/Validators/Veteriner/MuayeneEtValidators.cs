using FluentValidation;
using System;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Veteriner;

namespace VeterinerBilgiSistemi.Models.Validators.Veteriner
{
    public class MuayeneEtValidators : AbstractValidator<MuayeneEtViewModel>
    {
        public MuayeneEtValidators()
        {
            RuleFor(x => x.SonrakiMuayeneTarihi)
                .NotEmpty().WithMessage("Lütfen sonraki muayene tarihini giriniz.")
                .NotNull().WithMessage("Lütfen sonraki muayene tarihini giriniz.")
                .Must(x => x.HasValue && x.Value.Date >= DateTime.Now.Date)
                .WithMessage("Sonraki muayene tarihi, bugunden önce olamaz.");

            RuleForEach(x => x.MuayendeYapilanKanTestleri)
                .Must(kanTesti => !kanTesti.KanDegeriValue.HasValue || kanTesti.SeciliMi)
                .WithName("MuayendeYapilanKanTestleri[{CollectionIndex}]")
                .WithMessage("Lütfen seçim yapınız.");

            RuleForEach(x => x.MuayendeYapilanKanTestleri)
                .Must(kanTesti => !kanTesti.SeciliMi || kanTesti.KanDegeriValue.HasValue)
                .WithName("MuayendeYapilanKanTestleri[{CollectionIndex}]")
                .WithMessage("Seçim yapıldı ancak test sonucu girilmedi.");

            RuleForEach(x => x.MuayendeYapilanKanTestleri)
                .Must(kanTesti => !kanTesti.SeciliMi || (kanTesti.KanDegeriValue.HasValue && kanTesti.KanDegeriValue > 0))
                .WithName("MuayendeYapilanKanTestleri[{CollectionIndex}]")
                .WithMessage("Kan değeri pozitif bir değer olmalıdır.");

            RuleForEach(x=>x.MuayenedeKullanilanStoklar)
                .Must(stok => !stok.StokCikisAdet.HasValue || stok.SeciliMi)
                .WithName("MuayenedeKullanilanStoklar[{CollectionIndex}]")
                .WithMessage("Lütfen seçim yapınız.");

            RuleForEach(x => x.MuayenedeKullanilanStoklar)
                .Must(stok => !stok.SeciliMi || stok.StokCikisAdet.HasValue)
                .WithName("MuayenedeKullanilanStoklar[{CollectionIndex}]")
                .WithMessage("Seçim yapıldı ancak miktar girilmedi.");

            RuleForEach(x => x.MuayenedeKullanilanStoklar)
                .Must(stok => !stok.SeciliMi || (stok.StokCikisAdet.HasValue && stok.StokCikisAdet > 0))
                .WithName("MuayenedeKullanilanStoklar[{CollectionIndex}]")
                .WithMessage("Stok miktarı pozitif bir değer olmalıdır.");

            RuleForEach(x => x.MuayenedeKullanilanStoklar)
                .Must((model, stok) => 
                    (stok.StokCikisAdet.HasValue && stok.StokCikisAdet > 0 &&  stok.SeciliMi) ? 
                    FunctionsValidator.BePositiveStock(stok.StokId, stok.StokCikisAdet.Value) : true
                )
                .WithName("MuayenedeKullanilanStoklar[{CollectionIndex}]")
                .WithMessage("Girilen miktara göre stok miktarı negatif olmaktadır.\nGirilen miktarı veya deponuzda bulunan stok sayısını kontrol ediniz.");



            RuleFor(x => x.HastalikId)
                .NotNull().WithMessage("Lütfen hayvanın sağlık durumunu giriniz.")
                .NotEmpty().WithMessage("Lütfen hayvanın sağlık durumunu giriniz.")
                .Must(FunctionsValidator.BeHastalik).WithMessage("Seçilen hastalık sistemde tanımlı değil.");


        }
    }
}
