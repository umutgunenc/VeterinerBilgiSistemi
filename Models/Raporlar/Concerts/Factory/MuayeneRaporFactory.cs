using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts.Factory
{
    public class MuayeneRaporFactory : IRaporFactory
    {

        private int? _doktorId;
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;

        public MuayeneRaporFactory(VeterinerDBContext context, JsonSerializerOptions options,  int? doktorId = null)
        {
            _doktorId = doktorId;
            _context = context;
            _options = options;
        }

        public IRapor CreateRapor(int? doktorId = null)
        {
            // Eğer doktor seçilmişse, doktor bazında rapor dönecek
            if (_doktorId.HasValue)
                return new DoktorGunlukMuayeneRaporu(_doktorId.Value, _options, _context);
            else
                // Tüm muayeneleri göster
                return new KimKacMuayeneYaptiRapor(_context, _options);
        }
        public object GetRapor(DateTime ilkTarih, DateTime sonTarih)
        {
            var rapor = CreateRapor();  // CreateRapor metodunu çağırıyoruz
            return rapor.GetRaporAsync(ilkTarih, sonTarih);  // Oluşturduğumuz raporun GetRapor metodunu çağırıyoruz
        }
    }
}
