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

        public async Task<KanTahlilleriniGetirViewModel> SecilenKanTahlilDetayınıGetirAsync(VeterinerDBContext context)
        {
            var secilenKanTahlili = await context.KanDegerleri.Where(x => x.KanDegerleriId == KanDegerleriId).FirstOrDefaultAsync();

            this.AktifMi = secilenKanTahlili.AktifMi;
            this.AltLimit = secilenKanTahlili.AltLimit;
            this.UstLimit = secilenKanTahlili.UstLimit;
            this.KanTestiAdi = secilenKanTahlili.KanTestiAdi;
            this.KanDegerleriId = secilenKanTahlili.KanDegerleriId;
            this.KanTestiBirimi = secilenKanTahlili.KanTestiBirimi;
            this.Imza = ImzaOlustur(secilenKanTahlili.KanDegerleriId.ToString(), secilenKanTahlili.KanDegerleriId.ToString());

            return this;
        }

        public async Task<KanDegerleri> KanTahlilBilgileriniDuzenle(VeterinerDBContext context)
        {
            var DuzenlenecekKanTahlili = await context.KanDegerleri
                                                    .Where(x => x.KanDegerleriId == KanDegerleriId)
                                                    .FirstOrDefaultAsync();

            DuzenlenecekKanTahlili.AktifMi = AktifMi;
            DuzenlenecekKanTahlili.KanTestiAdi = KanTestiAdi;
            DuzenlenecekKanTahlili.AltLimit = AltLimit;
            DuzenlenecekKanTahlili.UstLimit = UstLimit;
            DuzenlenecekKanTahlili.KanTestiBirimi = KanTestiBirimi;

            return DuzenlenecekKanTahlili;
        }

        public async Task<KanDegerleri> SilinecekKanTahliliniGetirAsync(VeterinerDBContext context)
        {
            return await context.KanDegerleri.Where(x => x.KanDegerleriId == KanDegerleriId).FirstOrDefaultAsync();
        }

        private string ImzaOlustur(string string1, string string2)
        {
            Imza = Signature.CreateSignature(string1, string2);
            return Imza;
        }
    }
}
