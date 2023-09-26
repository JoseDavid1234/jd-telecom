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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sprache;

namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class ClienteController : Controller
    {

        private readonly TicketServiceImplement _ticketService;
        private readonly UsuarioServiceImplement _usuarioService;
        private readonly ReciboServiceImplement _reciboService;
        private readonly ApplicationDbContext _context;
        private Timer _timer;


        public ClienteController(TicketServiceImplement ticketService,UsuarioServiceImplement usuarioService,ReciboServiceImplement reciboService,ApplicationDbContext context)
        {
            _ticketService = ticketService;
            _usuarioService = usuarioService;
            _reciboService = reciboService;
            
            _context = context;
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

        [Authorize(Roles ="C")]
        [HttpGet("RecibosPagados")]
        public async Task<IActionResult> RecibosPagados()
        {
            var idUserClaim = User.FindFirst("idUser").Value;
            if(idUserClaim != null){
                int idUser = int.Parse(idUserClaim);
 
                var recibosPagados = await _reciboService.GetAllCompletedVouchers(idUser);
                var recibosPendientes = await _reciboService.GetAllPendingVouchers(idUser);

                var recibos = new DobleLista<Recibos,Recibos>(recibosPagados,recibosPendientes);

                return View("RecibosPagados",recibos);

            }else{
                return View("Error");
            }
        }

        [Authorize(Roles ="C")]
        [HttpPost]
        public async Task<IActionResult> RecibosPagadosPorMes(string mes)
        {
            var idUserClaim = User.FindFirst("idUser").Value;
            if(idUserClaim != null){
                int idUser = int.Parse(idUserClaim);
                Console.WriteLine(mes);
                var recibosPagados = await _context.DB_Recibos.Include(r=>r.usuario).Where(recibos=>recibos.mes_recibo == mes && recibos.usuario.id_usuario==idUser && recibos.estado_recibo=="PAGADO").ToListAsync();
                var recibosPendientes = await _reciboService.GetAllPendingVouchers(idUser);

                if(mes == "Todos"){
                    recibosPagados = await _context.DB_Recibos.Include(r=>r.usuario).Where(recibos=>recibos.usuario.id_usuario==idUser && recibos.estado_recibo=="PAGADO").ToListAsync();
                }

                var recibos = new DobleLista<Recibos,Recibos>(recibosPagados,recibosPendientes);
                return View("RecibosPagados",recibos);
            }
            Console.WriteLine("No se encontro un usuario");
            return View("Error");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}