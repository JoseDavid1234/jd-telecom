using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using JDTelecomunicaciones.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JDTelecomunicaciones.Services
{
    public class ReciboHostedService : IHostedService
    {
        private System.Threading.Timer? _timer2;
        //private readonly ReciboServiceImplement _recibosService;
        //private readonly ServicesFactory _serviceFactory;
        private IServiceProvider _serviceProvider;

        public ReciboHostedService(IServiceProvider serviceProvider)
        {
            //cls_serviceFactory = serviceFactory;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //TimeSpan.FromDays(1)
            //_timer = new Timer(/*aasync state=>{await GenerarRecibo(state);}*/message, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            Console.WriteLine("MESSAGE STARTASYNC");
            _timer2 = new System.Threading.Timer(async state=>{await GenerarRecibo(state);}, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
            

        }

        public void message(object state){
            Console.WriteLine("MESSAGE");
        }

        private async Task GenerarRecibo(object state){

            Console.WriteLine("SE EJECUTO GENERAR RECIBO");
            DateTime fechaActual = DateTime.Now;
            DateTime fechaGeneracionRecibo = new DateTime(fechaActual.Year,fechaActual.Month,24);;
            DateTime fechaVencimiento = new DateTime(fechaActual.Year, fechaActual.Month, 30);
            string nombreMes = fechaActual.ToString("MMMM");
            
            if(fechaActual >= fechaGeneracionRecibo){
                using (var scope = _serviceProvider.CreateScope()){
                    var _reciboService = scope.ServiceProvider.GetRequiredService<ReciboServiceImplement>();
                    var _usuarioService = scope.ServiceProvider.GetRequiredService<UsuarioServiceImplement>();
                    try{
                            var recibos = await _reciboService.GetAllMonthlyUserVouchers(1,"");
                            var usuarios = await _usuarioService.GetUsers();
                            foreach(var usuario in usuarios){

                                    if(usuario != null){
                                        var recibo = new Recibos{plan_recibo="JD_BASICO",mes_recibo=nombreMes,fecha_vencimiento=fechaVencimiento.ToString("d/MM/yyyy"),monto_recibo=30.00m,estado_recibo="PENDIENTE",usuario = usuario};

                                        await _reciboService.AddVoucher(recibo);
                                        Console.WriteLine("SE AÑADIO EL RECIBO AL USUARIO : " + usuario.nombre_usuario + " " + usuario.id_usuario);
                                    }
                            }

                    }catch(Exception e){
                        Console.WriteLine("ERROR: " + e.Message);
                        
                    }
                }
            }
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer2?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    


    }
}
