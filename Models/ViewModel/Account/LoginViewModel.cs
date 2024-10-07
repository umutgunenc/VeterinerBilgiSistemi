using Microsoft.AspNetCore.Mvc;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Account
{
    public class LoginViewModel : AppUser
    {
        public int rolId { get; set; }

    }
}
