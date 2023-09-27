using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace JDTelecomunicaciones.Services
{
    public class PromocionServiceImplement : IPromocionesService
    {

        private readonly ApplicationDbContext _context;
        public PromocionServiceImplement(ApplicationDbContext context){
            _context = context;
        }

        public async Task AddPromotion(Promocion promocion)
        {
            await _context.DB_Promociones.AddAsync(promocion);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePromotion(int id)
        {
            var promocion = await _context.DB_Promociones.FindAsync(id);
            if(promocion == null){
                throw new Exception("No se encontro la promocion");
            }
            _context.DB_Promociones.Remove(promocion);
            await _context.SaveChangesAsync();
        }

        public Task EditPromotion(int id , Promocion promocion)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Promocion>> GetAllPromotions()
        {
            var promociones = await _context.DB_Promociones.ToListAsync();
            return promociones;
        }

        public async Task<Promocion> GetPromotionById(int id)
        {
            var promocion = await _context.DB_Promociones.FindAsync(id);
            if (promocion == null){
                throw new Exception("La promoción no se encontró o no existe.");
            }

            return promocion;
        }
    }
}