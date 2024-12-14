using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;

namespace VeterinerBilgiSistemi.Models.ViewModel.Raporlar
{
    public class MuayeneRaporlariViewModel :Raporlar
    {
        public override async Task<List<SelectListItem>> SelectListItemsGetirAsync(VeterinerDBContext context)
        {
            SelectListItems = await context.AppUsers
                .Where(x => x.DiplomaNo != null)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.InsanAdi.ToUpper()} {x.InsanSoyadi.ToUpper()}"
                })
                .ToListAsync();
            return SelectListItems;
        }
    }
}
