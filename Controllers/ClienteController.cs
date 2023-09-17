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

        private readonly TicketServiceImplement _ticketService;
        private readonly UsuarioServiceImplement _usuarioService;

        public ClienteController(TicketServiceImplement ticketService,UsuarioServiceImplement usuarioService)
        {
            _ticketService = ticketService;
            _usuarioService = usuarioService;
        }
        [Authorize(Roles ="C")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="C")]
        [HttpGet("ServicioTecnico")]
        public async Task<IActionResult> ServicioTecnico()
        {
            var idUserClaim = User.FindFirst("idUser").Value;
            int idUser = int.Parse(idUserClaim);

            var miUsuario = await _usuarioService.FindUserById(idUser);
            var tickets = await _ticketService.GetTicketsById(idUser);

            Console.WriteLine(miUsuario.persona + " <-- AQUI HAY UNA PERSONA ");

            var modeloConListas = new ModeloConListas<Usuario,Tickets>(miUsuario,tickets);

            return View("ServicioTecnico",modeloConListas);
        }
        [Authorize(Roles ="C")]
        [HttpPost("EnviarTicket")]
        public async Task<IActionResult> EnviarTicket(string tipoProblematica,string descripcion){
            DateTime fechaActual = DateTime.Today;
            string fechaActualS = fechaActual.ToString("dd/MM/yyyy");
            var idUserClaim = User.FindFirst("idUser").Value;
            int idUser = int.Parse(idUserClaim);
            var miUsuario = await _usuarioService.FindUserById(idUser);

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