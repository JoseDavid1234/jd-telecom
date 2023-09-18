using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class AdministracionController : Controller
    {
        private readonly ILogger<AdministracionController> _logger;

        public AdministracionController(ILogger<AdministracionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaClientes()
        {
            return View("ListaClientes");
        }

        public IActionResult Promociones()
        {
            return View("Promociones");
        }
        [HttpPost]
        public IActionResult AñadirPromocion(){

            


            return RedirectToAction("Promociones");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}