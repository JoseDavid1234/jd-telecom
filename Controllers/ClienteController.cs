using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using JDTelecomunicaciones.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sprache;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly TicketServiceImplement _ticketService;
        private readonly UsuarioServiceImplement _usuarioService;
        private readonly ApplicationDbContext _context;

        public ClienteController(ILogger<ClienteController> logger,TicketServiceImplement ticketService, ApplicationDbContext context,UsuarioServiceImplement usuarioService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _usuarioService = usuarioService;
            _context = context;
        }
        [Authorize(Roles ="C")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="C")]
        [HttpGet("ServicioTecnico")]
        public IActionResult ServicioTecnico()
        {
            var tickets = _ticketService.GetTickets();
            foreach(var item in tickets.Result){
                Console.WriteLine(item.id_ticket + " " + item.tipoProblematica_ticket + " " + item.status_ticket);
            }
            return View("ServicioTecnico",tickets.Result);
        }
        [Authorize(Roles ="C")]
        [HttpPost("EnviarTicket")]
        public async Task<IActionResult> EnviarTicket(string tipoProblematica,string descripcion){
            DateTime fechaActual = DateTime.Today;
            string fechaActualS = fechaActual.ToString("dd/MM/yyyy");
            Console.WriteLine(fechaActualS);
            var miUsuario = await _usuarioService.FindUserById(1);

            var ticket = new Tickets{ tipoProblematica_ticket = tipoProblematica,descripcion_ticket = descripcion,status_ticket = "PENDIENTE",usuario =miUsuario ,fecha_ticket = fechaActualS};
            _ticketService.AddTickets(ticket);
            return RedirectToAction("ServicioTecnico");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}