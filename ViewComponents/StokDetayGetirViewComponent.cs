using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.ViewComponents
{
    public class StokDetayGetirViewComponent : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync(StokDuzenleKaydetViewModel model)
        {
            if (model == null)
            {
                return View("StokDuzenle");
            }            

            return View(model);
        }
    }
}
