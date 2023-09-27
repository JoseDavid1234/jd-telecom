using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Services
{
    public interface IPromocionesService
    {
        public Task<List<Promocion>> GetAllPromotions();
        public Task<Promocion> GetPromotionById(int id);
        public Task AddPromotion(Promocion promocion);
        public Task DeletePromotion(int id);
        public Task EditPromotion(int id,Promocion promocion);
        
    }
}