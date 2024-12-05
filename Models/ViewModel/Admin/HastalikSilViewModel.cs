using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class HastalikSilViewModel :Hastalik
    {
        public List<SelectListItem> HastalikListesi { get; set; }

        public async Task<List<SelectListItem>> HastalikListesiniGetirAsync(VeterinerDBContext context)
        {
            HastalikListesi = new();
            var hastaliklar = await context.Hastaliklar.ToListAsync();
            foreach (var hastalik in hastaliklar)
            {
                HastalikListesi.Add(new SelectListItem
                {
                    Text = hastalik.HastalikAdi,
                    Value=hastalik.HastalikId.ToString()
                });
            }
            return HastalikListesi;
        }

        public async Task<Hastalik> SilinecekHastaligiGetirAsnyc(VeterinerDBContext context)
        {
            return await context.Hastaliklar.FindAsync(HastalikId); 
        }



    }
}
