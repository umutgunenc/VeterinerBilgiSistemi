using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts
{
    public class MuayenedekiHastaliklar : IRapor
    {
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;


        public MuayenedekiHastaliklar(VeterinerDBContext context, JsonSerializerOptions options)
        {
            _context = context;
            _options = options;
        }

        public async Task<string> GetRaporAsync(DateTime ilkTarih, DateTime sonTarih)
        {

            var query = await _context.Muayeneler
                .Where(m => m.MuayeneTarihi <= sonTarih && m.MuayeneTarihi >= ilkTarih)
                .Include(m => m.Hastalik)
                .ToListAsync();


            var result = query
                .GroupBy(m => m.HastalikId)
                .Select(g => new
                {
                    HastalikAdi = g.FirstOrDefault().Hastalik.HastalikAdi.ToUpper(),
                    HastalikSayisi = g.Count()
                })
                .ToList();


            string jsonResult = JsonSerializer.Serialize(result, _options); 
            return jsonResult;
        }
    }
}
