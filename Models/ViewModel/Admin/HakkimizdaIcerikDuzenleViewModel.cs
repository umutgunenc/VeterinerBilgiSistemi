using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class HakkimizdaIcerikDuzenleViewModel : Hakkimizda
    {
        public string Imza { get; set; }
        public List<HakkimizdaIcerikDuzenleViewModel> Icerikler { get; set; }

        public async Task<List<HakkimizdaIcerikDuzenleViewModel>> IcerikleriGetirAsync(VeterinerDBContext context)
        {
            var icerikler = await context.Hakkimizda.ToListAsync();
            Icerikler = new();

            foreach (var icerik in icerikler)
            {
                var viewModel = new HakkimizdaIcerikDuzenleViewModel
                {
                    HakkimizdaId = icerik.HakkimizdaId,
                    Aciklama=icerik.Aciklama,
                    Baslik = icerik.Baslik,
                    AktifMi=icerik.AktifMi,
                    Imza = Signature.CreateSignature(icerik.HakkimizdaId.ToString(), icerik.HakkimizdaId.ToString())
                };
                Icerikler.Add(viewModel);
            }
            return Icerikler;
        }
    }
}
