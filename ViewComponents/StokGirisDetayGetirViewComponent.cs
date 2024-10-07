using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.ViewComponents
{
    public class StokGirisDetayGetirViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(StokGirisKaydetViewModel model)
        {
            if (model == null)
                return View("StokGiris");

            return View(model);
        }
    }
}
