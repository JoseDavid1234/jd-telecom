using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using JDTelecomunicaciones.Hubs;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace JDTelecomunicaciones.Controllers
{
    public class SorteosController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<SpinHub> _hubContext;

        public SorteosController(IHubContext<SpinHub> hubContext,IHttpClientFactory clientFactory,ApplicationDbContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
           _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessDni(MensajeContacto mc)
        {

            var contact = new MensajeContacto{
              DNI = mc.DNI,
              CorreoElectronico = mc.CorreoElectronico,
              NumeroTelefono = mc.NumeroTelefono
            };
            var apiKey = Environment.GetEnvironmentVariable("API_DNI");
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, 
                "https://dniruc.apisperu.com/api/v1/dni/" + mc.DNI + "?token="+apiKey);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                  var responseStream = await response.Content.ReadAsStringAsync();
                  var data = JsonDocument.Parse(responseStream);
                  var nombres = data.RootElement.GetProperty("nombres").GetString();

                  contact.NombreCompleto=nombres;
                  _context.ContactMessages.Add(contact);


                  await _context.SaveChangesAsync();

                  // Retrieve client list from the database
                  var clients = await _context.ContactMessages.ToListAsync();

                  // Send new client list to all connected clients
                  await _hubContext.Clients.All.SendAsync("ClientsUpdated", clients);
                }
                catch (System.Exception)
                {
                  return Json(new { exitoso = false });
                }
            }
            else
            {
                //algo salió mal con la solicitud
            }

            return Json(new { exitoso = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            // Retrieve client list from the database
            var clients = await _context.ContactMessages.ToListAsync();
            Console.WriteLine(clients);

            return Json(clients);
        }
    }
}