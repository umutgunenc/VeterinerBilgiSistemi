using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class FotografDuzenleViewModel : AnaSayfaFotograflar
    {
        public List<FotografDuzenleViewModel> FotograflarListesi { get; set; }

        public string Imza { get; set; }

        public async Task<List<FotografDuzenleViewModel>> FotograflariGetirAsync(VeterinerDBContext context)
        {
            var fotograflar = await context.AnaSayfaFotograflar.ToListAsync();
            FotograflarListesi = new ();

            foreach (var fotograf in fotograflar)
            {
                var viewModel = new FotografDuzenleViewModel
                {
                    AnaSayfaFotograflarId = fotograf.AnaSayfaFotograflarId,
                    Url = fotograf.Url,
                    FotografAdi = fotograf.FotografAdi,
                    AktifMi = fotograf.AktifMi,
                    Imza = Signature.CreateSignature(fotograf.AnaSayfaFotograflarId.ToString(), fotograf.AnaSayfaFotograflarId.ToString())
                };
                FotograflarListesi.Add(viewModel);
            }

            return FotograflarListesi;
        }

    }
}
