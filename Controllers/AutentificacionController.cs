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

                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role,  user.rol_usuario.ToString()),
                    new Claim("idUser",user.id_usuario.ToString())
                    
                };
                Console.WriteLine(username+" "+user.rol_usuario.ToString()+" "+user.id_usuario.ToString());
               

                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
                Console.WriteLine(claims[1].ToString());

                /*String rolUser = User.FindFirstValue(ClaimTypes.Role);
                Console.WriteLine(rolUser);*/

                switch(user.rol_usuario){
                    case 'A': 
                        return RedirectToAction("Index","Admin");                  
                    case 'C':
                        return RedirectToAction("ServicioTecnico","Cliente");                   
                    case 'T':
                        return RedirectToAction("Index","Tecnico");
                    default:
                        return RedirectToAction("Error","Home");                        
                }
            }
            return View("IniciarSesion");
        }

        [Route("/Logout")]
        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }

        [HttpGet("Registrarse")]
        public IActionResult Registrarse()
        {
            return View("Registrar");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string dni, string password,string confirmPassword,string Nombre,string apPaterno,string apMaterno,string correo,char sexo)
        {   
            try{  
                if(password==confirmPassword){
                    var persona = new Persona {nombrePersona= Nombre,apPatPersona=apPaterno,apMatPersona=apMaterno,dniPersona=dni,sexoPersona=sexo};
                    var usuario = new Usuario {correo_usuario=correo,nombre_usuario=dni,contraseña_usuario=password,rol_usuario='C',persona=persona};
                    await _usuarioService.AddUser(usuario);
                }else{
                    Console.WriteLine("Contraseña diferente");
                }
                
                return View("IniciarSesion");

            }catch(Exception ex){

                Console.WriteLine(ex);
                return RedirectToAction("Error","Home");
            }


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}