using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts
{
    public class EnCokSatilanStok : IRapor
    {
        private readonly VeterinerDBContext _context;
        private readonly JsonSerializerOptions _options;


        public EnCokSatilanStok(VeterinerDBContext context, JsonSerializerOptions options)
        {
            _context = context;
            _options = options;
        }

        public async Task<string> GetRaporAsync(DateTime ilkTarih, DateTime sonTarih)
        {

            var query = await _context.StokHareketler
                .Include(sh=>sh.Stok)
                .Where(sh => sh.SatisTarihi != null && sh.SatisTarihi >= ilkTarih && sh.SatisTarihi <= sonTarih)
                .ToListAsync();

            var result= query
                .GroupBy(sh => sh.StokId)
                .Select(g => new
                {
                    SatisSayisi = g.Sum(sh=>sh.StokCikisAdet),
                    StokAdi = g.FirstOrDefault().Stok.StokAdi.ToUpper()
                })
                .ToList();



            string jsonResult = JsonSerializer.Serialize(result, _options);

            return jsonResult;
        }
    }
}
