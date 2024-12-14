using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts
{
    public class HastalikHayvanDagilimRapor : IRapor
    {
        private int _hastalikId;
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;


        public HastalikHayvanDagilimRapor(int hastalikId, VeterinerDBContext context, JsonSerializerOptions options)
        {
            _hastalikId = hastalikId;
            _context = context;
            _options = options;
        }

        public async Task<string> GetRaporAsync(DateTime ilkTarih, DateTime sonTarih)
        {

            //var query = _context.Muayeneler
            //                    .Where(m => m.MuayeneTarihi >= ilkTarih && m.MuayeneTarihi <= sonTarih && m.HastalikId == _hastalikId)
            //                    .Include(m => m.Hastalik)
            //                    .Include(m => m.Hayvan)
            //                        .ThenInclude(h => h.CinsTur)
            //                        .ThenInclude(ct => ct.Cins)
            //                    .Include(m => m.Hayvan)
            //                        .ThenInclude(h => h.CinsTur)
            //                        .ThenInclude(ct => ct.Tur)
            //                    .GroupBy(h => h.Hayvan.CinsTurId)
            //                    .Select(g => new
            //                    {
            //                        HastalikAdi = g.Select(m => m.Hastalik.HastalikAdi.ToUpper()),
            //                        HayvanTuru = g.Select(m => m.Hayvan.CinsTur.Tur.TurAdi).FirstOrDefault().ToUpper(),
            //                        HayvanCinsi = g.Select(m => m.Hayvan.CinsTur.Cins.CinsAdi).FirstOrDefault().ToUpper(),
            //                        HayvanSayisi = g.Count()
            //                    });


            //var resault = await query
            //    .ToListAsync();

            var query = await _context.Muayeneler
                .Where(m => m.MuayeneTarihi >= ilkTarih && m.MuayeneTarihi <= sonTarih && m.HastalikId == _hastalikId)
                .Include(m => m.Hastalik)
                .Include(m => m.Hayvan)
                    .ThenInclude(h => h.CinsTur)
                    .ThenInclude(ct => ct.Cins)
                .Include(m => m.Hayvan)
                    .ThenInclude(h => h.CinsTur)
                    .ThenInclude(ct => ct.Tur)
                .ToListAsync();


            var result = query
                .GroupBy(h => h.Hayvan.CinsTurId)
                .Select(g => new
                {
                    HastalikAdi = g.First().Hastalik.HastalikAdi.ToUpper(),
                    HayvanTuru = g.First().Hayvan.CinsTur.Tur.TurAdi.ToUpper(),
                    HayvanCinsi = g.First().Hayvan.CinsTur.Cins.CinsAdi.ToUpper(),
                    HayvanSayisi = g.Count()
                })
                .ToList();

            string jsonResult = JsonSerializer.Serialize(result, _options);
            return jsonResult;

        }
    }
}
