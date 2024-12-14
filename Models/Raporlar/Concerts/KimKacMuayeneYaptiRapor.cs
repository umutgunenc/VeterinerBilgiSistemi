using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts
{
    public class KimKacMuayeneYaptiRapor : IRapor
    {
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;

        public KimKacMuayeneYaptiRapor(VeterinerDBContext context, JsonSerializerOptions options)
        {
            _context = context;
            _options = options;
        }

        public async Task<string> GetRaporAsync(DateTime ilkTarih, DateTime sonTarih)
        {

            var query = await _context.Muayeneler
                .Where(m => m.MuayeneTarihi >= ilkTarih && m.MuayeneTarihi <= sonTarih)
                .Include(m => m.Hekim)
                .ToListAsync();


            var result =  query
                .GroupBy(m => m.HekimId)
                .Select(g => new
                {
                    HekimAdı = g.FirstOrDefault().Hekim.InsanAdi.ToUpper(),
                    HekimSoyadi = g.FirstOrDefault().Hekim.InsanSoyadi.ToUpper(),
                    MuayeneSayisi = g.Count()
                })
                .ToList();


            string jsonResult = JsonSerializer.Serialize(result, _options); 
            return jsonResult;
        }


    }
}
