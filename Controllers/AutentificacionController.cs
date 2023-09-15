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
    public class AutentificacionController : Controller
    {
        private readonly ILogger<AutentificacionController> _logger;

        public AutentificacionController(ILogger<AutentificacionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("IniciarSesion")]
        public IActionResult IniciarSesion()
        {
            return View("IniciarSesion");
        }

        [HttpGet("Registrarse")]
        public IActionResult Registrarse()
        {
            return View("Registrar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}