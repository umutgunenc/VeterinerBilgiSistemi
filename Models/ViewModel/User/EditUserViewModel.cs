using Microsoft.AspNetCore.Http;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.User
{
    public class EditUserViewModel : AppUser
    {
        public IFormFile filePhoto { get; set; }
        public string PhotoOption { get; set; }

    }
}
