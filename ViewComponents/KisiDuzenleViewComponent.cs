using Microsoft.AspNetCore.Mvc;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VeterinerBilgiSistemi.ViewComponents
{
    public class KisiDuzenleViewComponent : ViewComponent
    {   private readonly VeterinerDBContext _context;

        public KisiDuzenleViewComponent(VeterinerDBContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(KisiDuzenleViewModel model)
        {
            
            if (model == null)
            {
                return View("KisiSec");
            }
            //model = await model.SecilenKisiyiGetirAsync(_context, model);
            return View(model);
        }
    }
}
