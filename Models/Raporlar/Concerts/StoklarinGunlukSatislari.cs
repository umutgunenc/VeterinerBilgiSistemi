using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts
{
    public class StoklarinGunlukSatislari : IRapor
    {
        private int _stokId;
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;


        public StoklarinGunlukSatislari(int stokId, VeterinerDBContext context, JsonSerializerOptions options)
        {
            _stokId = stokId;
            _context = context;
            _options = options;
        }

        public async Task<string> GetRaporAsync(DateTime ilkTarih, DateTime sonTarih)
        {

            var query = await _context.StokHareketler
                .Where(sh => sh.SatisTarihi != null && sh.SatisTarihi <= sonTarih && sh.SatisTarihi >= ilkTarih && sh.StokId == _stokId)
                .Include(sh => sh.Stok)
                .ToListAsync();

            var result = query
                .GroupBy(sh => sh.SatisTarihi.Value.Date)
                .OrderBy(sh=>sh.FirstOrDefault().SatisTarihi)
                .Select(g => new
                {
                    StokAdi = g.FirstOrDefault().Stok.StokAdi.ToUpper(),
                    SatisTarihi = g.Key.ToString("dd.MM.yyyy"),
                    SatisSayisi = g.Sum(sh=>sh.StokCikisAdet),
                })
                .ToList();

            string jsonResult = JsonSerializer.Serialize(result, _options);

            return jsonResult;
        }
    }
}
