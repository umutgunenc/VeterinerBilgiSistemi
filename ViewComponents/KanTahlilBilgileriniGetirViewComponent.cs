using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.ViewComponents
{
    public class KanTahlilBilgileriniGetirViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(KanTahlilleriniGetirViewModel model)
        {
            return View(model);
        }
    }

}
