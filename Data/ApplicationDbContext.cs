using Microsoft.EntityFrameworkCore;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MensajeContacto> ContactMessages { get; set; }
        public DbSet<Usuario> Usuarios {get;set;}
        public DbSet<Tickets> Tickets {get;set;}
        public DbSet<Persona> Personas {get;set;}
        public DbSet<Promocion> Promociones {get;set;}
        public DbSet<Recibos> Recibos {get;set;}
    
    }
}