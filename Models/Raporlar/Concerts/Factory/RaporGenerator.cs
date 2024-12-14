using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Enum;
using VeterinerBilgiSistemi.Models.Raporlar.Abstract;

namespace VeterinerBilgiSistemi.Models.Raporlar.Concerts.Factory
{
    public class RaporGenerator
    {
        private readonly VeterinerDBContext _context;

        public RaporGenerator(VeterinerDBContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateReportAsync(RaporTuru raporTuru, int? id, DateTime ilkTarih, DateTime sonTarih)
        {
            IRaporFactory raporFactory = null;

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            switch (raporTuru)
            {
                case RaporTuru.Muayene:
                    raporFactory = new MuayeneRaporFactory(_context, options, id);
                    break;
                case RaporTuru.Stok:
                    raporFactory = new StokRaporFactory(_context, options, id);
                    break;
                case RaporTuru.Hastalik:
                    raporFactory = new HastalikRaporFactory(_context, options, id);
                    break;
                default:
                    new ArgumentException("Geçersiz rapor türü");
                    break;
            }


            var rapor = raporFactory.CreateRapor();
            var raporVerisi = await rapor.GetRaporAsync(ilkTarih, sonTarih);

            return raporVerisi;
        }
    }
}
