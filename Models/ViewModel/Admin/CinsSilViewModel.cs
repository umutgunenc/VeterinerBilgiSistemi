﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class CinsSilViewModel : Cins
    {

        public List<SelectListItem> CinslerListesi { get; set; }

        public async Task<List<SelectListItem>> CinslerListesiGetirAsync(VeterinerDBContext context)
        {
            var cinsler = await context.Cinsler.ToListAsync();
            CinslerListesi = new();
            foreach (var cins in cinsler)
            {
                CinslerListesi.Add(new SelectListItem
                {
                    Text = cins.CinsAdi,
                    Value = cins.CinsId.ToString()
                });
            }
            return CinslerListesi;
        }

        public async Task<Cins> SilinecekCinsiGetir(VeterinerDBContext context)
        {
            return await context.Cinsler.FindAsync(CinsId);
        }
    }
}
