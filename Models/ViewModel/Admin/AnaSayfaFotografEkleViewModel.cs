using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class AnaSayfaFotografEkleViewModel :AnaSayfaFotograflar
    {
        public IFormFile FilePhoto { get; set; }


    }
}
