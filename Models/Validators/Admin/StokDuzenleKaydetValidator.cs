﻿using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class StokDuzenleKaydetValidator :AbstractValidator<StokDuzenleKaydetViewModel>
    {
        public StokDuzenleKaydetValidator()
        {
            RuleFor(x => x.StokAdi)
                .NotEmpty().WithMessage("Stok adı boş olamaz.")
                .NotNull().WithMessage("Stok adı boş olamaz.")
                .MaximumLength(50).WithMessage("Stok adı en fazla 50 karakter olabilir.")
                .Must((model,StokAdi)=>FunctionsValidator.BeUniqueStokAdi(model.Id,StokAdi)).WithMessage("Sistem aynı isimde bir stok bulunmaktadır.");

            RuleFor(x => x.BirimId)
                .NotNull().WithMessage("Birim seçimi yapılmalıdır.")
                .NotEmpty().WithMessage("Birim seçimi yapılmalıdır.")
                .Must(FunctionsValidator.BeBirim).WithMessage("Seçilen birim sistemde kayıtlı değil.");

            RuleFor(x => x.KategoriId)
                .NotNull().WithMessage("Kategori seçimi yapılmalıdır.")
                .NotEmpty().WithMessage("Kategori seçimi yapılmalıdır.")
                .Must(FunctionsValidator.BeKategori).WithMessage("Seçilen kategori sistemde kayıtlı değil.");


            RuleFor(x => x.AktifMi)
                .Must(x => x == true || x == false).WithMessage("Lütfen Evet veya Hayır seçeneğini işaretleyiniz.");

            RuleFor(x => x.StokBarkod)
                .NotNull().WithMessage("Barkod boş olamaz.")
                .NotEmpty().WithMessage("Barkod boş olamaz.")
                .MaximumLength(50).WithMessage("Barkod en fazla 50 karakter olabilir.")
                .Must((model, StokBarkod) => FunctionsValidator.BeUniqueBarkod(model.Id, StokBarkod)).WithMessage("Girilen barkod numarası başka bir stoğa aittir.");
        }
    }
}