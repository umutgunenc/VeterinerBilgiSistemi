using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class TurSilViewModel :Tur
    {


        public List<SelectListItem> TurListesi { get; set; }

        public async Task<List<SelectListItem>> TurListesiniGetirASync(VeterinerDBContext context)
        {
            var turler =await context.Turler.ToListAsync();
            TurListesi = new();
            foreach (var tur in turler)
            {
                TurListesi.Add(new SelectListItem
                {
                    Text = tur.TurAdi,
                    Value = tur.TurId.ToString()
                });
            }
            return TurListesi;
        }

        public async Task<Tur> SilinecekTuruGetirAsync(VeterinerDBContext context)
        {
            return await context.Turler.FindAsync(TurId);
        }


    }
}
