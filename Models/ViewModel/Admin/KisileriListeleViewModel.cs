﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;

namespace VeterinerBilgiSistemi.Models.ViewModel.Admin
{
    public class KisileriListeleViewModel : AppUser
    {

        public string RolAdi { get; set; }
        public AppUser SecilenKisi { get; set; }

        public IQueryable<KisileriListeleViewModel> Kisiler { get; set; }


        public async Task<KisileriListeleViewModel> SecilenKisiyiGetirAsync(string tckn, VeterinerDBContext context)
        {
            return await context.AppUsers.Where(x => x.InsanTckn == tckn).Select(kisi => new KisileriListeleViewModel
            {
                InsanAdi = kisi.InsanAdi,
                InsanSoyadi = kisi.InsanSoyadi,
                InsanTckn = kisi.InsanTckn,
                Email = kisi.Email.ToLower(),
                PhoneNumber = kisi.PhoneNumber,
                DiplomaNo = kisi.DiplomaNo,
                UserName = kisi.UserName,
                CalisiyorMu = kisi.CalisiyorMu,
                RolAdi = context.Roles
                        .Where(r => r.Id == context.UserRoles
                                            .Where(ur => ur.UserId == kisi.Id)
                                            .Select(ur => ur.RoleId)
                                            .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
            }).FirstOrDefaultAsync();
        }

        public IQueryable<KisileriListeleViewModel> KisiListesiniGetir(VeterinerDBContext context)
        {
            Kisiler = context.Users.Select(kisi => new KisileriListeleViewModel
            {
                InsanTckn = kisi.InsanTckn,
                InsanAdi = kisi.InsanAdi,
                InsanSoyadi = kisi.InsanSoyadi,
                CalisiyorMu = kisi.CalisiyorMu,
                Email = kisi.Email,
                PhoneNumber = kisi.PhoneNumber,
                RolAdi = context.Roles
                        .Where(r => r.Id == context.UserRoles
                                            .Where(ur => ur.UserId == kisi.Id)
                                            .Select(ur => ur.RoleId)
                                            .FirstOrDefault())
                        .Select(r => r.Name)
                        .FirstOrDefault()
            });

            return Kisiler;

        }
    }
}
