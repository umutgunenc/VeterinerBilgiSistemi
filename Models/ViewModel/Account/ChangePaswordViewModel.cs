using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Account
{
    public class ChangePaswordViewModel : AppUser
    {
        private readonly VeterinerDBContext _context;

        public ChangePaswordViewModel()
        {

        }
        public ChangePaswordViewModel(VeterinerDBContext context)
        {
            _context = context;
        }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
