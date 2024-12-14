using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;

namespace VeterinerBilgiSistemi.Models.ViewModel.Raporlar
{
    public class HastalikRaporlariViewModel :Raporlar
    {
        public override async Task<List<SelectListItem>> SelectListItemsGetirAsync(VeterinerDBContext context)
        {
            var query = context.Hastaliklar
                .Select(h => new SelectListItem{
                    Value= h.HastalikId.ToString(),
                    Text = h.HastalikAdi 
                });
            SelectListItems = await query.ToListAsync();
            return SelectListItems;
        }
    }
}
