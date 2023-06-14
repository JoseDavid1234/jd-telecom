using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace JDTelecomunicaciones.Hubs
{
  public class SpinHub : Hub
  {

      private readonly ApplicationDbContext _context;

      public SpinHub(ApplicationDbContext context)
      {
          _context = context;
      }
      public async Task Spin()
      {
        var clients= await _context.ContactMessages.ToListAsync();
        var random = new Random();
        var speed = random.NextDouble()*0.5+0.1;
        await Clients.Others.SendAsync("Spin",clients,speed);
      }
  }
}