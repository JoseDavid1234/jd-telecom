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
                var recibos = await _context.DB_Recibos.Where(r=> r.usuario.id_usuario == idUser).ToListAsync();
                //await GenerarRecibo();
                return View("RecibosPagados",recibos);

            }else{
                return View("Error");
            }
        }

        [Authorize(Roles ="C")]
        [HttpPost]
        public async Task<IActionResult> RecibosPagadosPorMes(int userId,string mes)
        {
            var recibos = await _context.DB_Recibos.Where(recibos=>recibos.mes_recibo == mes && recibos.usuario.id_usuario==userId).ToListAsync();
            return View("RecibosPagados",recibos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        /*
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //TimeSpan.FromDays(1)
            _timer = new Timer(async state=>{ await GenerarRecibo(state);}, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            
            return Task.CompletedTask;

        }

        private async Task GenerarRecibo(object state){
            try{

                Console.WriteLine("SE EJECUTO GENERAR RECIBO");
                DateTime fechaActual = DateTime.Now;

                    DateTime fechaVencimiento = new DateTime(fechaActual.Year, fechaActual.Month, 30);
                    string nombreMes = fechaActual.ToString("MMMM");
                    var idUserClaim = User.FindFirst("idUser").Value;
                    int idUser = int.Parse(idUserClaim);
                    var miUsuario = await _usuarioService.FindUserById(idUser);


                    var recibo = new Recibos{plan_recibo="JD_BASICO",mes_recibo=nombreMes,fecha_vencimiento=fechaVencimiento.ToString("d/MM/yyyy"),monto_recibo=30.00m,estado_recibo="PENDIENTE",usuario = miUsuario};

                    await _reciboService.AddVoucher(recibo);
            }catch(Exception e){
                Console.WriteLine("ERROR: " + e.Message);
                
            }

        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        */
    }
}