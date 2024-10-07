using Microsoft.AspNetCore.Mvc;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Animal
{
    public class MuayeneViewModel : Muayene
    {
        public string HekimAdi { get; set; }
    }
}
