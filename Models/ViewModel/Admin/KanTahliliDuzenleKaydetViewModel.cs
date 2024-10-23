using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class KanTahliliDuzenleKaydetViewModel :KanDegerleri
    {
        public KanDegerleri DuzenlenecekKanTahlili { get; set; }

        public async Task<KanDegerleri> KanTahlilBilgileriniDuzenle(SecilenKanTahlilDetayiViewModel model, VeterinerDBContext context)
        {
            DuzenlenecekKanTahlili = await context.KanDegerleri.Where(x => x.KanDegerleriId == model.KanDegerleriId).FirstOrDefaultAsync();

            DuzenlenecekKanTahlili.AktifMi = model.AktifMi;
            DuzenlenecekKanTahlili.KanTestiAdi = model.KanTestiAdi;
            DuzenlenecekKanTahlili.AltLimit = model.AltLimit;
            DuzenlenecekKanTahlili.UstLimit = model.UstLimit;
            DuzenlenecekKanTahlili.KanTestiBirimi = model.KanTestiBirimi;

            return DuzenlenecekKanTahlili;
        }


    }
}
