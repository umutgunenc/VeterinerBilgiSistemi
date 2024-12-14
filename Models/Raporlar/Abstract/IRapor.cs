using System;
using System.Threading.Tasks;

namespace VeterinerBilgiSistemi.Models.Raporlar.Abstract
{
    public interface IRapor
    {
        Task<string> GetRaporAsync(DateTime ilkTarih, DateTime sonTarih);

    }
}
