using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Services
{
    public interface ITicketService
    {
        
        public Task<List<Tickets>> GetTickets();
        public Task AddTickets(Tickets ticket);
        public Task DeleteTicketById(int id);
        public Task EditTicket(int id,Tickets ticket);

    }
}