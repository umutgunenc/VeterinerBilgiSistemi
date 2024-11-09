using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.ViewModel.Veteriner;

namespace VeterinerBilgiSistemi.ViewComponents
{
    public class HayvanBilgileriniGetirViewComponent :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Hayvan model)
        {
            return View();
        }
    }
}
