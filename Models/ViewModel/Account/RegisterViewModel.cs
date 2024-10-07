using Microsoft.AspNetCore.Http;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Account
{
    public class RegisterViewModel : AppUser
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int rolId { get; set; }
        public IFormFile filePhoto { get; set; }
    }
}
