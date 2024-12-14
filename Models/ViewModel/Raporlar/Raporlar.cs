using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Enum;

namespace VeterinerBilgiSistemi.Models.ViewModel.Raporlar
{
    public abstract class Raporlar
    {
        public List<SelectListItem> SelectListItems { get; set; }
        public DateTime? IlkTarih { get; set; }
        public DateTime? SonTarih { get; set; }
        public string RaporVerisi { get; set; }
        public RaporTuru RaporTuru { get; set; }
        public int? Id { get; set; }
        public abstract Task<List<SelectListItem>> SelectListItemsGetirAsync(VeterinerDBContext context);   

    }
}
