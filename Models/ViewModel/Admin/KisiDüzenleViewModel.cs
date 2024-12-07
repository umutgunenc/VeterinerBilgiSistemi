using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class KisiDuzenleViewModel : AppUser
    {

        private AppRole Rol { get; set; }
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public List<SelectListItem> RollerListesi { get; set; }
        public string Signature { get; set; }
        public KisiDuzenleViewModel SecilenKisi { get; set; }
        public IdentityUserRole<int> EskiRol { get; set; }
        public IdentityUserRole<int> YeniRol { get; set; }
        public AppUser UpdateOlacakKullanici { get; set; }


        public async Task<IdentityUserRole<int>> KullanicininEskiRolunuGetirAsync(VeterinerDBContext context)
        {

            return await context.UserRoles.Where(ur => ur.UserId == Id).FirstOrDefaultAsync();


        }
        public IdentityUserRole<int> KullanicininYeniRolunuGetir(KisiDuzenleViewModel model)
        {
            return new IdentityUserRole<int>
            {
                UserId = model.Id,
                RoleId = model.RolId
            };

        }
        public async Task<List<SelectListItem>> RollerListesiniGetirAsync(VeterinerDBContext context)
        {
            RollerListesi = new();
            var roller = await context.Roles.ToListAsync();

            foreach (var rol in roller)
            {
                RollerListesi.Add(new SelectListItem
                {
                    Text = rol.Name,
                    Value = rol.Id.ToString()
                });
            }
            return RollerListesi;
        }
        public async Task<KisiDuzenleViewModel> SecilenKisiyiGetirAsync(VeterinerDBContext context, KisiSecViewModel model)
        {
            var secilenKisi = await context.Users
                .Where(u => u.InsanTckn == model.InsanTckn)
                .FirstOrDefaultAsync();

            this.Id = secilenKisi.Id;
            this.InsanAdi = secilenKisi.InsanAdi;
            this.InsanSoyadi = secilenKisi.InsanSoyadi;
            this.CalisiyorMu = secilenKisi.CalisiyorMu;
            this.InsanTckn = secilenKisi.InsanTckn;
            this.Email = secilenKisi.Email;
            this.PhoneNumber = secilenKisi.PhoneNumber;
            this.DiplomaNo = secilenKisi.DiplomaNo;
            this.UserName = secilenKisi.UserName;
            this.RollerListesi = await RollerListesiniGetirAsync(context);
            this.Rol = await context.Roles
                 .Where(r => r.Id == context.UserRoles
                       .Where(ur => ur.UserId == secilenKisi.Id)
                       .Select(ur => ur.RoleId)
                       .FirstOrDefault())
                 .FirstOrDefaultAsync();
            this.RolId = this.Rol.Id;
            this.RolAdi = this.Rol.Name;

            return this;
        }
        public async Task<AppUser> UpdateOlacakKullaniciyiGetirAsync(VeterinerDBContext context)
        {
            var UpdateOlacakKullanici = await context.AppUsers.FindAsync(Id);

            UpdateOlacakKullanici.InsanAdi = InsanAdi.ToUpper();
            UpdateOlacakKullanici.InsanSoyadi = InsanSoyadi.ToUpper();
            UpdateOlacakKullanici.CalisiyorMu = CalisiyorMu;
            UpdateOlacakKullanici.Email = Email.ToLower();
            UpdateOlacakKullanici.PhoneNumber = PhoneNumber;
            UpdateOlacakKullanici.DiplomaNo = DiplomaNo;
            UpdateOlacakKullanici.UserName = UserName.ToUpper();

            return UpdateOlacakKullanici;
        }

    }
}


