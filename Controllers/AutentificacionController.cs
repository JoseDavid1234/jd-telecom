using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using JDTelecomunicaciones.Services;
using JDTelecomunicaciones.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace JDTelecomunicaciones.Controllers
{
    [Route("[controller]")]
    public class AutentificacionController : Controller
    {
        private readonly ILogger<AutentificacionController> _logger;
        private readonly UsuarioServiceImplement _usuarioService;

        public AutentificacionController(ILogger<AutentificacionController> logger , UsuarioServiceImplement usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet("IniciarSesion")]
        public IActionResult IniciarSesion()
        {
            return View("IniciarSesion");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {   
            var user = _usuarioService.FindUserByCredentials(username,password).Result;
            
            if(user != null){
                Console.WriteLine("Se encontro un usuario");

                //

                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role,  user.rol_usuario.ToString()),
                    new Claim("idUser",user.id_usuario.ToString())
                    
                };
                Console.WriteLine(username+" "+user.rol_usuario.ToString()+" "+user.id_usuario.ToString());
                //
                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
                Console.WriteLine(claims[1].ToString());

                /*String rolUser = User.FindFirstValue(ClaimTypes.Role);
                Console.WriteLine(rolUser);*/

                switch(user.rol_usuario){
                    case 'A': 
                        return RedirectToAction("Index","Admin",new {userId=user.id_usuario,username=username,userRol=user.rol_usuario});                  
                    case 'C':
                        return RedirectToAction("IndexUsuario","Usuario");                   
                    case 'T':
                        return RedirectToAction("Index","Repartidor");
                    default:
                        return RedirectToAction("Error","Home");                        
                }
            }
            
            

            return View("IniciarSesion");
        }



        [HttpGet("Registrarse")]
        public IActionResult Registrarse()
        {
            return View("Registrar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}