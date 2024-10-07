using Microsoft.AspNetCore.Http;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.User
{
    public class FaceIdViewModel :AppUser
    {
        public IFormFile[] filePhoto { get; set; }
    }
}
