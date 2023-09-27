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

        [HttpGet("Index")]
        public IActionResult Index()
        {
            Console.WriteLine("INDEX ADMIN");
            return View("Index");
        }

        [HttpGet("ListaClientes")]
        public IActionResult ListaClientes()
        {
            return View("ListaClientes");
        }

        [HttpGet("Promociones")]
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