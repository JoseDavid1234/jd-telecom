using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace JDTelecomunicaciones.Hubs
{
  public class SpinHub : Hub
  {
      public async Task Spin()
      {
          await Clients.Others.SendAsync("Spin");
      }
  }
}