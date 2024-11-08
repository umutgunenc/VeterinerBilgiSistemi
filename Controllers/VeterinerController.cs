using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Controllers
{
    [Authorize(Roles = "VETERİNER,VETERINER")]
    public class VeterinerController : Controller
    {
        private readonly VeterinerDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public VeterinerController(VeterinerDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult VeterinerIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MuayeneEt()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MuayeneKayitlari()
        {
            return View();
        }

    }
}
