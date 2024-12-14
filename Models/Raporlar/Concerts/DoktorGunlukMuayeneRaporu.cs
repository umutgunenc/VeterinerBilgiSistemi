using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts
{
    public class DoktorGunlukMuayeneRaporu : IRapor
    {
        private int _doktorId;
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;

        public DoktorGunlukMuayeneRaporu(int doktorId, JsonSerializerOptions options, VeterinerDBContext context)
        {
            _doktorId = doktorId;
            _context = context;
            _options = options;
        }

        public async Task<string> GetRaporAsync(DateTime ilkTarih, DateTime sonTarih)
        {

            var query = await _context.Muayeneler
                .Where(m => m.MuayeneTarihi <= sonTarih && m.MuayeneTarihi >= ilkTarih && m.HekimId == _doktorId)
                .Include(m => m.Hekim)
                .ToListAsync();

            var result= query
                .GroupBy(m => m.MuayeneTarihi.Date)
                .OrderBy(g=>g.FirstOrDefault().MuayeneTarihi)
                .Select(g => new
                {
                    HekimAdi = g.FirstOrDefault().Hekim.InsanAdi.ToUpper(),
                    HekimSoyadi = g.FirstOrDefault().Hekim.InsanSoyadi.ToUpper(),
                    MuayeneTarihi = g.Key.ToString("dd.MM.yyyy"),
                    MuayeneSayisi = g.Count()
                });


            string jsonResult = JsonSerializer.Serialize(result, _options);

            return jsonResult;
        }
    }
}
