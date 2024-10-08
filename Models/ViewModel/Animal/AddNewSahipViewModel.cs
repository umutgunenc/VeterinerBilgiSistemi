using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;


namespace VeterinerBilgiSistemi.Models.ViewModel.Animal
{
    public class AddNewSahipViewModel : Hayvan
    {
        public string YeniSahipTCKN { get; set; }
        public string UserTCKN { get; set; }
        public string TurAdi { get; set; }
        public string CinsAdi { get; set; }
        public string RenkAdi { get; set; }
        public string Signature { get; set; }
        public AppUser YeniSahip { get; set; }

        public async Task<string> RenkAdiniGetirAsync(VeterinerDBContext context, Hayvan hayvan)
        {
            return await context.Renkler.Where(r => r.RenkId == hayvan.RenkId).Select(r => r.RenkAdi).FirstOrDefaultAsync();
        }
        public async Task<string> CinsAdiniGetirAsync(VeterinerDBContext context, Hayvan hayvan)
        {
            return await context.Cinsler
                .Where(c => c.CinsId == context.CinsTur
                    .Where(ct => ct.Id == hayvan.CinsTurId)
                    .Select(ct => ct.CinsId)
                    .FirstOrDefault())
                .Select(c => c.CinsAdi)
                .FirstOrDefaultAsync();
        }
        public async Task<string> TurAdiniGetirAsync(VeterinerDBContext context, Hayvan hayvan)
        {
            return await context.Turler
                .Where(t => t.TurId == context.CinsTur
                    .Where(ct => ct.Id == hayvan.CinsTurId)
                    .Select(ct => ct.TurId)
                    .FirstOrDefault())
                .Select(t => t.TurAdi)
                .FirstOrDefaultAsync();
        }
        public async Task<AddNewSahipViewModel> ViewModelOlusturAsync(Hayvan hayvan, AppUser User,VeterinerDBContext context)
        {
            return new AddNewSahipViewModel()
            {
                YeniSahipTCKN = "",
                HayvanId = hayvan.HayvanId,
                HayvanAdi = hayvan.HayvanAdi,
                ImgUrl = hayvan.ImgUrl,
                HayvanDogumTarihi = hayvan.HayvanDogumTarihi,
                HayvanOlumTarihi = hayvan.HayvanOlumTarihi,
                RenkAdi = await RenkAdiniGetirAsync(context,hayvan),
                CinsAdi = await CinsAdiniGetirAsync(context,hayvan),
                TurAdi = await TurAdiniGetirAsync(context,hayvan),
                HayvanCinsiyet = hayvan.HayvanCinsiyet,
                HayvanKilo = hayvan.HayvanKilo,
                UserTCKN = User.InsanTckn,
                Signature = VeterinerBilgiSistemi.Fonksiyonlar.Signature.CreateSignature(hayvan.HayvanId.ToString(),User.InsanTckn)

            };
        }

        
    }
}
