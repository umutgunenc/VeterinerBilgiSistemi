using Microsoft.AspNetCore.Mvc;
using VeterinerBilgiSistemi.Data;
using System.Linq;
using VeterinerBilgiSistemi.Models.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace VeterinerBilgiSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly VeterinerDBContext _context;
        public HomeController(VeterinerDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new List<AnaSayfaFotograflar>();
            model = await _context.AnaSayfaFotograflar.Where(x => x.AktifMi == true).ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> IndexData()
        {
            var model = new List<AnaSayfaFotograflar>();
            model = await _context.AnaSayfaFotograflar.Where(x=>x.AktifMi==true).ToListAsync();
            return Json(model);
        }

        public async Task<IActionResult> Hakkimizda()
        {
            List<Hakkimizda> hakkimizdaListesi = await _context.Hakkimizda
                .Where(x=>x.AktifMi==true)
                .ToListAsync();
            List<IletisimBilgileri> iletisimBilgileriListesi = await _context.IletisimBilgileri
                .Include(ib => ib.SosyalMedyalar)
                .ThenInclude(ibsm => ibsm.SosyalMedya)
                .ToListAsync();

            var tupple = (hakkimizdaListesi, iletisimBilgileriListesi);

            return View(tupple);
        }

        
    }
}
