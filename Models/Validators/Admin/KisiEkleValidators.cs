﻿using FluentValidation;
using System.Collections.Generic;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Data;
using System;
using System.Linq;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public partial class KisiEkleValidators : AbstractValidator<KisiEkleViewModel>
    {
        public KisiEkleValidators()
        {

            RuleFor(x => x.InsanTckn)
                .NotEmpty().WithMessage("Lütfen TCKN giriniz.")
                .NotNull().WithMessage("Lütfen TCKN giriniz.")
                .Length(11).WithMessage("TCKN 11 karakter uzunluğunda olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TCKN numarası sadece rakamlardan oluşmalıdır.")
                .Must(FunctionsValidator.BeUniqueTCKN).WithMessage("Girilen TCKN zaten sistemde kayıtlı.")
                .Must(FunctionsValidator.BeValidTCKN).WithMessage("Geçerli bir TCKN giriniz.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir mail adresi giriniz.")
                .NotNull().WithMessage("Lütfen e-mail adresi giriniz.")
                .NotEmpty().WithMessage("Lütfen e-mail adresi giriniz.")
                .MaximumLength(100).WithMessage("e-mail adresi maksimum 100 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeUniqueEmail).WithMessage("Girilen e-posta adresi zaten sisteme kayıtlı.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telefon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.")
                .Must(FunctionsValidator.BeUniqueTel).WithMessage("Girilen telefon numarası zaten sisteme kayıtlı");

            RuleFor(x => x.DiplomaNo)
                .MaximumLength(11).WithMessage("Diploma numarası en fazla 11 karakter olabilir.")
                .Must(FunctionsValidator.BeUniqueOrNullDiplomaNo).WithMessage("Girilen diploma numarası zaten sistemde kayıtlı.");

            RuleFor(x => x.DiplomaNo)
                .NotNull().WithMessage("Diploma numarası giriniz.")
                .When(x => FunctionsValidator.IsRoleMatching(x.RolId, new List<string> { "veteriner" }));

            RuleFor(x => x.InsanAdi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda isim girilebilir.")
                .NotEmpty().WithMessage("Çalışanın ismini giriniz.")
                .NotNull().WithMessage("Çalışanın ismini giriniz.");

            RuleFor(x => x.InsanSoyadi)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda soyisim girilebilir.")
                .NotEmpty().WithMessage("Çalışanın soyismini giriniz.")
                .NotNull().WithMessage("Çalışanın soyismini giriniz.");

            RuleFor(x => x.UserName)
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda kullanıcı adı girilebilir")
                .NotEmpty().WithMessage("Çalışanın kullanıcı adını giriniz.")
                .NotNull().WithMessage("Çalışanın kullanıcı adını giriniz.")
                .Must(FunctionsValidator.BeUniqueKullaniciAdi).WithMessage("Sistemde bu isimde bir kullanici adi mevcut. Farkli bir kullanıcı adı seçiniz.");

            RuleFor(x => x.RolId)
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.")
               .NotNull().WithMessage("Çalışan için bir görev seçiniz.");

        }


    }
}
