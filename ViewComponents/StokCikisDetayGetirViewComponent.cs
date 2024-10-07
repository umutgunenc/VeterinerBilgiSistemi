using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.ViewComponents
{
    public class StokCikisDetayGetirViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(StokCikisKaydetViewModel model)
        {

            if (model == null)
            {
                return View("StokCikis");
            }

            return View(model);

        }
    }
}
