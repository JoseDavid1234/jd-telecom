using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Services
{
    public class ReciboServiceImplement : IRecibosService
    {
    
        private readonly ApplicationDbContext _context;
        public ReciboServiceImplement(ApplicationDbContext context){
            _context = context;
        }

        public async Task AddVoucher(Recibos recibo)
        {
            try{
                if(recibo != null){
                    await _context.DB_Recibos.AddAsync(recibo);
                    await _context.SaveChangesAsync();
                }
            }catch(Exception e){
                Console.WriteLine("ERROR : " + e.Message);
            }
            
        }

        public Task DeleteVoucher(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recibos>> GetAllMonthlyUserVouchers(int userId, string mes)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recibos>> GetAllUserVouchers(int userId)
        {
            throw new NotImplementedException();
        }

        public Recibos GetVoucherById(int voucherId)
        {
            throw new NotImplementedException();
        }
    }
}