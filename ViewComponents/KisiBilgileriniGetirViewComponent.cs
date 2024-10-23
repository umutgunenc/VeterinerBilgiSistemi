using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

public class KisiBilgileriniGetirViewComponent : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync(KisileriListeleViewModel model)
    {
        return View(model);
    }
}
