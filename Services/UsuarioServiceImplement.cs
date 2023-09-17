using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Data;

using JDTelecomunicaciones.Models;
using Microsoft.EntityFrameworkCore;

namespace JDTelecomunicaciones.Services
{
    public class UsuarioServiceImplement
    {
        private readonly ApplicationDbContext _context;

    public UsuarioServiceImplement(ApplicationDbContext context){
        _context = context;
    }
    public async Task<Usuario> FindUserById(int id){
        try{
            var usuario = await _context.DB_Usuarios
            .Include(u => u.persona)
            .FirstOrDefaultAsync(u => u.id_usuario == id);
            
            return usuario;
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return null;
        }
    }
    public async Task<List<Usuario>> GetUsers(){
        var usuarios = await _context.DB_Usuarios.ToListAsync();
        return usuarios;
    }

    public async Task AddUser(Usuario usuario){
        try{
            await _context.DB_Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

        }catch(Exception e){
            Console.WriteLine(e.Message);
        }

    }

    public async Task DeleteUserById(int id){
        try{
            var usuario = await _context.DB_Usuarios.FindAsync(id);
            if(usuario != null){
                _context.DB_Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }

    public async Task EditUser(int id,Usuario usuario){
        try{
            var userToChange = await _context.DB_Usuarios.FindAsync(id);
            if(userToChange != null){
                userToChange.nombre_usuario = usuario.nombre_usuario;
                userToChange.correo_usuario = usuario.correo_usuario;
                userToChange.rol_usuario = usuario.rol_usuario;
                userToChange.contraseña_usuario = usuario.contraseña_usuario;
                await _context.SaveChangesAsync();
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
    public async Task<Usuario> FindUserByCredentials(string username, string password){
        try{
            var usuario = await _context.DB_Usuarios.FirstOrDefaultAsync(u=>u.nombre_usuario == username && u.contraseña_usuario == password);
            if(usuario != null){
                return usuario;
            }
            return null;
        }catch(Exception e){
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    
    }
}