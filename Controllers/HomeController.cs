using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly ApplicationDbContext _context;

  public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
  {
      _logger = logger;
      _context = context;

  }

  public IActionResult Index()
  {
      return View();
  }

  public IActionResult Privacy()
  {
      return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }

  [HttpPost]
  public IActionResult EnviarFormulario(MensajeContacto message)
  {
    _context.ContactMessages.Add(message);
    _context.SaveChanges();

    // Devuelve una respuesta JSON en lugar de una vista
    return Json(new { exitoso = true });
  }
}
