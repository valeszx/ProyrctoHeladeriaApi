using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Heladeria.Repositorio.IRepositorio;

namespace ProyectoHeladeria1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepositorio _repos; 
        public LoginController(ILoginRepositorio repos)
        {
            _repos = repos;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var listadoLogin = _repos.GetLogins();
            return Ok(listadoLogin);
        }
        [HttpGet("{usuario}/{password}/validar")]
        public IActionResult validarUsuario(String usuario,String password) 
        {
            var valido = _repos.ValidarLogin(usuario, password);
            return Ok(valido);

        }
        
    }
}
