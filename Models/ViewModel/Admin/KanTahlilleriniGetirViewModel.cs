using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Fonksiyonlar;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class KanTahlilleriniGetirViewModel : KanDegerleri
    {
        public string Imza { get; set; }
        public List<SelectListItem> KanTahlilleriListesi { get; set; }

        async public Task<List<SelectListItem>> KanTahlilleriListesiniGetirAsnyc(VeterinerDBContext context)
        {
            KanTahlilleriListesi = new();
            var kanDegerleri = await context.KanDegerleri.ToListAsync();

            foreach (var item in kanDegerleri)
            {
                KanTahlilleriListesi.Add(new SelectListItem
                {
                    Text = item.KanTestiAdi,
                    Value = item.KanDegerleriId.ToString()
                });

            }
            return KanTahlilleriListesi;
        }

        public async Task<KanTahlilleriniGetirViewModel> SecilenKanTahlilDetayınıGetirAsync(KanTahlilleriniGetirViewModel model, VeterinerDBContext context)
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

        public async Task<KanDegerleri> KanTahlilBilgileriniDuzenle(KanTahlilleriniGetirViewModel model, VeterinerDBContext context)
        {
            var DuzenlenecekKanTahlili = await context.KanDegerleri
                                                    .Where(x => x.KanDegerleriId == model.KanDegerleriId)
                                                    .FirstOrDefaultAsync();

            DuzenlenecekKanTahlili.AktifMi = model.AktifMi;
            DuzenlenecekKanTahlili.KanTestiAdi = model.KanTestiAdi;
            DuzenlenecekKanTahlili.AltLimit = model.AltLimit;
            DuzenlenecekKanTahlili.UstLimit = model.UstLimit;
            DuzenlenecekKanTahlili.KanTestiBirimi = model.KanTestiBirimi;

            return DuzenlenecekKanTahlili;
        }

        public async Task<KanDegerleri> SilinecekKanTahliliniGetirAsync(KanTahlilleriniGetirViewModel model, VeterinerDBContext context)
        {
            return await context.KanDegerleri.Where(x => x.KanDegerleriId == model.KanDegerleriId).FirstOrDefaultAsync();
        }

        private string ImzaOlustur(string string1, string string2)
        {
            Imza = Signature.CreateSignature(string1, string2);
            return Imza;
        }
    }
}
