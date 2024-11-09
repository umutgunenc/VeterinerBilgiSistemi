using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Models.ViewModel.Veteriner;

namespace VeterinerBilgiSistemi.ViewComponents
{
    public class KisininHayvanlariniGetirViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(KisiSecViewModel model)
        {
            return View(model);
        }

    }
}
