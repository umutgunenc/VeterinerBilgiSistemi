using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;

namespace VeterinerBilgiSistemi.Models.ViewModel.Raporlar
{
    public class StokRaporlariViewModel : Raporlar
    {
        public override async Task<List<SelectListItem>> SelectListItemsGetirAsync(VeterinerDBContext context)
        {
            SelectListItems =await context.Stoklar
                .Select(s => new SelectListItem
                {
                    Text = s.StokAdi.ToUpper(),
                    Value = s.Id.ToString()
                })
                .ToListAsync();

            return SelectListItems;
        }
    }
}
