using System;
using System.Text.Json;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts.Factory
{
    public class HastalikRaporFactory : IRaporFactory
    {
        private int? _hastalikId;
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;


        public HastalikRaporFactory(VeterinerDBContext context, JsonSerializerOptions oprtions, int ? hastalikId = null)
        {
            _hastalikId = hastalikId;
            _context = context;
            _options = oprtions;
        }

        public IRapor CreateRapor(int? hastalikId = null)
        {
            // Eğer hastalık seçilmişse, hastalık bazında rapor dönecek
            if (_hastalikId.HasValue)
                return new HastalikHayvanDagilimRapor(_hastalikId.Value, _context, _options);
            else
                // Tüm hastalıkları göster
                return new MuayenedekiHastaliklar(_context, _options);
        }
        public object GetRapor(DateTime ilkTarih, DateTime sonTarih)
        {
            var rapor = CreateRapor();
            return rapor.GetRaporAsync(ilkTarih, sonTarih);
        }
    }
}
