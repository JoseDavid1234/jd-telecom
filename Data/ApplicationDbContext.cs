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
    }
}