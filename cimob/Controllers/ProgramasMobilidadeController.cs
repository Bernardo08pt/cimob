﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace cimob.Controllers
{
    public class ProgramasMobilidadeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        //Método que retorna o modal dos programas de mobilidade
        public ActionResult ModalPopUp()
        {
            return View();
        }
    }
}