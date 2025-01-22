using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Enum;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;
using VeterinerBilgiSistemi.Models.Raporlar.Concerts.Factory;
using VeterinerBilgiSistemi.Models.ViewModel.Raporlar;

namespace VeterinerBilgiSistemi.Controllers
{
    [Authorize(Roles = "ADMIN")]

    public class RaporlarController : Controller
    {
        private readonly VeterinerDBContext _context;

        public RaporlarController(VeterinerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> StokRaporlari()
        {
            Raporlar rapor = new StokRaporlariViewModel();
            rapor.SelectListItems = await rapor.SelectListItemsGetirAsync(_context);
            rapor.RaporTuru = RaporTuru.Stok;
            return View(rapor);
        }

        [HttpGet]
        public async Task<IActionResult> MuayeneRaporlari()
        {
            Raporlar rapor = new MuayeneRaporlariViewModel();
            rapor.SelectListItems = await rapor.SelectListItemsGetirAsync(_context);
            rapor.RaporTuru = RaporTuru.Muayene;
            return View(rapor);
        }

        [HttpGet]
        public async Task<IActionResult> HastalikRaporlari()
        {
            Raporlar rapor = new HastalikRaporlariViewModel();
            rapor.SelectListItems = await rapor.SelectListItemsGetirAsync(_context);
            rapor.RaporTuru = RaporTuru.Hastalik;
            return View(rapor);
        }

        [HttpPost]
        public async Task<IActionResult> RaporSonucu(RaporTuru raporTuru, DateTime? ilkTarih, DateTime? sonTarih, int? id)
        {
            if (ilkTarih == null)
                ilkTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (sonTarih == null)
                sonTarih = DateTime.Now;

            Raporlar rapor = raporTuru switch
            {
                RaporTuru.Hastalik => new HastalikRaporlariViewModel(),
                RaporTuru.Muayene => new MuayeneRaporlariViewModel(),
                RaporTuru.Stok => new StokRaporlariViewModel(),
                _ => throw new ArgumentOutOfRangeException()
            };

            rapor.IlkTarih = ilkTarih;
            rapor.SonTarih = sonTarih;
            rapor.RaporTuru = raporTuru;
            rapor.Id = id;

            RaporGenerator raporGenerator = new(_context);
            rapor.SelectListItems = await rapor.SelectListItemsGetirAsync(_context);
            rapor.RaporVerisi = await raporGenerator.GenerateReportAsync(rapor.RaporTuru, rapor.Id, rapor.IlkTarih.Value, rapor.SonTarih.Value);

            switch (rapor.RaporTuru)
            {
                case RaporTuru.Muayene:
                    return View("MuayeneRaporlari", rapor);
                case RaporTuru.Stok:
                    return View("StokRaporlari", rapor);
                case RaporTuru.Hastalik:
                    return View("HastalikRaporlari",rapor);
                default:
                    return View("BadRequest");
            }

        }
    }
}
