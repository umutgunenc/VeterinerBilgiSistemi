using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class RenkSilViewModel :Renk
    {
        public List<SelectListItem> RenklerListesi { get; set; }

        public async Task<List<SelectListItem>> RenklerListesiniGetirAsync(VeterinerDBContext context)
        {
            var Renkler = await context.Renkler.ToListAsync();
            RenklerListesi = new();

            foreach (var renk in Renkler)
            {
                RenklerListesi.Add(new SelectListItem
                {
                    Text = renk.RenkAdi,
                    Value = renk.RenkId.ToString()
                });
            }
            return RenklerListesi;
        }

        public async Task<Renk> SilinecekRengiGetirAsync(VeterinerDBContext context)
        {
            return await context.Renkler.FindAsync(RenkId);
        }


    }
}
