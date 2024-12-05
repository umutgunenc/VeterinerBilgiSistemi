using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class CinsTurEslesmeKaldirViewModel : CinsTur
    {

        public List<SelectListItem> CinslerTurlerListesi { get; set; }
        public CinsTur EslemesiKaldiralacakCinstur { get; set; }

        public async Task<List<SelectListItem>> CinslerTurlerListesiniGetirAsync(VeterinerDBContext context)
        {
            var cinsTurler = await context.CinsTur.ToListAsync();
            var turler = await context.Turler.ToListAsync();
            var cinsler = await context.Cinsler.ToListAsync();

            CinslerTurlerListesi = new List<SelectListItem>();

            foreach (var cinsTur in cinsTurler)
            {
                var tur = turler.FirstOrDefault(t => t.TurId == cinsTur.TurId);
                var cins = cinsler.FirstOrDefault(c => c.CinsId == cinsTur.CinsId);

                if (tur != null && cins != null)
                {
                    CinslerTurlerListesi.Add(new SelectListItem
                    {
                        Text = $"{cins.CinsAdi} - {tur.TurAdi}",
                        Value = cinsTur.Id.ToString()
                    });
                }
            }

            return CinslerTurlerListesi;
        }

        public async Task<CinsTur> EslesmesiKaldirilacakCinsTuruGetirAsync( VeterinerDBContext context)
        {
            return await context.CinsTur.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<string> EslesmesiKaldirilacakCinsAdiniGetirAsync( VeterinerDBContext context)
        {
            CinsTur cinsTur = await EslesmesiKaldirilacakCinsTuruGetirAsync( context);
            return await context.Cinsler.Where(c => c.CinsId == cinsTur.CinsId).Select(c => c.CinsAdi).FirstOrDefaultAsync();
        }

        public async Task<string> EslesmesiKaldirilacakTurAdiniGetirAsync( VeterinerDBContext context)
        {
            CinsTur cinsTur = await EslesmesiKaldirilacakCinsTuruGetirAsync( context);
            return await context.Turler.Where(t => t.TurId == cinsTur.TurId).Select(t => t.TurAdi).FirstOrDefaultAsync();
        }
    }
}