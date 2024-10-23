using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class SecilenKanTahlilDetayiViewModel : KanDegerleri
    {
        public KanTahlilleriniGetirViewModel ReturnModel { get; set; }
        public string Imza {get;set;}
        public async Task<SecilenKanTahlilDetayiViewModel> SecilenKanTahlilDetayınıGetirAsync(KanTahlilleriniGetirViewModel model, VeterinerDBContext context)
        {
            var secilenKanTahlili = await context.KanDegerleri.Where(x => x.KanDegerleriId == model.KanDegerleriId).FirstOrDefaultAsync();

            this.AktifMi = secilenKanTahlili.AktifMi;
            this.AltLimit = secilenKanTahlili.AltLimit;
            this.UstLimit = secilenKanTahlili.UstLimit;
            this.KanTestiAdi = secilenKanTahlili.KanTestiAdi;
            this.KanDegerleriId = secilenKanTahlili.KanDegerleriId;
            this.KanTestiBirimi = secilenKanTahlili.KanTestiBirimi;
            this.Imza = ImzaOlustur(secilenKanTahlili.KanDegerleriId.ToString(), secilenKanTahlili.KanDegerleriId.ToString());

            return this;
        }
        public async Task<KanTahlilleriniGetirViewModel> ReturnModelOlusturAsync(VeterinerDBContext context)
        {
            ReturnModel = new();
            ReturnModel.KanTahlilleriListesi = await ReturnModel.KanTahlilleriListesiniGetirAsnyc(context);
            return ReturnModel;
        }

        private string ImzaOlustur(string string1,string string2)
        {
            Imza = Signature.CreateSignature(string1, string2);
            return Imza;            
        }

        

    }
}
