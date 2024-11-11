using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Veteriner
{
    public class KisiSecVeterinerViewModel : AppUser
    {

        public List<SelectListItem> KisininHayvanlarininListesi { get; set; }
        public async Task<List<SelectListItem>> KisininHayvanlarininListesiniGetirAsync(VeterinerDBContext context, KisiSecVeterinerViewModel model)
        {
            var kisininHayvanlari = await context.Hayvanlar
                .Include(h => h.Sahipler)
                .ThenInclude(s => s.AppUser)
                .Include(h => h.CinsTur)
                .ThenInclude(ct => ct.Cins)
                .Include(h => h.CinsTur)
                .ThenInclude(ct => ct.Tur)
                .Where(h => h.Sahipler.Any(s => s.AppUser.InsanTckn == model.InsanTckn))
                .ToListAsync();

            KisininHayvanlarininListesi = kisininHayvanlari
                       .Select(hayvan => new SelectListItem
                       {
                           Value = hayvan.HayvanId.ToString(),
                           Text = $"{hayvan.HayvanAdi.ToUpper()} {hayvan.CinsTur.Cins.CinsAdi} {hayvan.CinsTur.Tur.TurAdi}"
                       })
                       .ToList();

            return KisininHayvanlarininListesi;

        }

        public async Task<(Hayvan, bool)> SecilenHayvanBilgileriniGetirAsync(string id, VeterinerDBContext context, string tckn)
        {
            if (!Int32.TryParse(id, out int hayvanId))
                return (null, false);

            var hayvan = await context.Hayvanlar
                .Include(h => h.Sahipler.Where(s => s.AppUser.InsanTckn == tckn))
                    .ThenInclude(s => s.AppUser)
                .Include(h => h.CinsTur)
                    .ThenInclude(ct => ct.Cins)
                .Include(h => h.CinsTur)
                    .ThenInclude(ct => ct.Tur)
                .Include(h => h.Renk)
                .Include(h => h.HayvanAnne)
                .Include(h => h.HayvanBaba)
                .FirstOrDefaultAsync(h => h.HayvanId == hayvanId);


            if (hayvan == null)
                return (null, false);

            return (hayvan, true);


        }
    }
}
