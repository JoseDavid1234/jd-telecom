using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDTelecomunicaciones.Services
{
    public class ServicesFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ServicesFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ReciboServiceImplement CreateReciboService()
        {
            return ActivatorUtilities.CreateInstance<ReciboServiceImplement>(serviceProvider);
        }
    }
}