using System.Threading.Tasks;
using System;

namespace VeterinerBilgiSistemi.Models.Raporlar.Abstract
{
    public interface IRaporFactory
    {
        IRapor CreateRapor(int? id = null);
        object GetRapor(DateTime ilkTarih, DateTime sonTarih);
    }
}
