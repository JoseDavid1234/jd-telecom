using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Services
{
    public interface IUsuarioService
    {
        public Task<List<Usuario>> GetUsers();
        public Task AddUser(Usuario usuario);
        public Task<Usuario> FindUserById(int id);
        public Task<Usuario> FindUserByCredentials(string username, string password);
        public Task DeleteUserById(int id);
        public Task EditUser(int id,Usuario usuario);
    }
}