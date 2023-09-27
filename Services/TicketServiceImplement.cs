using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace JDTelecomunicaciones.Services
{
    public class TicketServiceImplement:ITicketService{
    private readonly ApplicationDbContext _context;

    public TicketServiceImplement(ApplicationDbContext context){
        _context = context;
    }
    
    public async Task<List<Tickets>> GetTickets(){
        var tickets = await _context.DB_Tickets.ToListAsync();
        return tickets;
    }

    public async Task<List<Tickets>> GetTicketsById(int id){
        var tickets = await _context.DB_Tickets.Where(t=>t.usuario.id_usuario == id).ToListAsync();
        return tickets;
    }

    public async Task AddTickets(Tickets ticket){
        try{
            await _context.DB_Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

        }catch(Exception e){
            Console.WriteLine(e.Message);
        }

    }

    public async Task DeleteTicketById(int id){
        try{
            var ticket = await _context.DB_Tickets.FindAsync(id);
            if(ticket != null){
                _context.DB_Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }

    public async Task EditTicket(int id,Tickets ticket){
        try{
            var ticketToChange = await _context.DB_Tickets.FindAsync(id);
            if(ticket != null){
                ticketToChange.descripcion_ticket = ticket.descripcion_ticket;
                ticketToChange.fecha_ticket = ticket.fecha_ticket;
                ticketToChange.status_ticket = ticket.status_ticket;
                ticketToChange.tipoProblematica_ticket = ticket.tipoProblematica_ticket;
                await _context.SaveChangesAsync();
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
    
}
}