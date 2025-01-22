using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.ViewModel.YapayZeka;

namespace VeterinerBilgiSistemi.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class YapayZekaController : Controller
    {
        private readonly VeterinerDBContext _context;

        public YapayZekaController(VeterinerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult YapayZekayiEgit()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> YapayZekaSonuclari()
        {

            var toplamMuayeneSayisi = await _context.Muayeneler
                .Skip(2000)
                .CountAsync();

            var dogruTahminSayisi = await _context.Muayeneler
                .Skip(2000)
                .Where(m => m.YapayZekaTahminId == m.HastalikId)
                .CountAsync();

            var tahminOrani = (toplamMuayeneSayisi > 0) ? ((double)dogruTahminSayisi / toplamMuayeneSayisi) * 100 : 0;

            YapayZekaSonuclari sonuclar = new();
            sonuclar.MuayeneSayisi = toplamMuayeneSayisi;
            sonuclar.DogruTahminSayisi = dogruTahminSayisi;
            sonuclar.TahminOrani = tahminOrani.ToString("F2");

            return View(sonuclar);
        }
    }
}
