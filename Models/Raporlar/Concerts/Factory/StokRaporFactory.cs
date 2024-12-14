using System;
using System.Text.Json;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts.Factory
{
    public class StokRaporFactory : IRaporFactory
    {
        private int? _stokId;
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;


        public StokRaporFactory(VeterinerDBContext context , JsonSerializerOptions options, int? stokId = null)
        {
            _stokId = stokId;
            _context = context;
            _options = options;
        }

        public IRapor CreateRapor(int? stokId = null)
        {
            // Eğer stok seçilmişse, stok bazında rapor dönecek
            if (_stokId.HasValue)
                return new StoklarinGunlukSatislari(_stokId.Value, _context,_options);
            else
                // Tüm stokları göster
                return new EnCokSatilanStok(_context,_options);

        }
        public object GetRapor(DateTime ilkTarih, DateTime sonTarih)
        {
            var rapor = CreateRapor();
            return rapor.GetRaporAsync(ilkTarih, sonTarih);
        }
    }
}
