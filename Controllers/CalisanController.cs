﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinerBilgiSistemi.Data;

namespace VeterinerBilgiSistemi.Controllers
{
    [Authorize(Roles = "ÇALIŞAN")]
    public class CalisanController : Controller
    {

        private readonly VeterinerDBContext _context;

        public CalisanController(VeterinerDBContext context)
        {
            _context = context;
        }


    }
}
