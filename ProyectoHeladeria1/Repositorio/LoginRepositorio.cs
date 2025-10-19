using Microsoft.EntityFrameworkCore;
using Proyecto_Heladeria.Data;
using Proyecto_Heladeria.Modelo;
using Proyecto_Heladeria.Repositorio.IRepositorio;

namespace Proyecto_Heladeria.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        // readonly se utiliza para almacenar una instancia de la clase
        private readonly AplicationDbContext _context;

        public LoginRepositorio(AplicationDbContext context)
        {
                _context = context;
        }
        public ICollection<Login> GetLogins()
        {
           return _context.Logins.ToList();
        }

        public bool ValidarLogin(string usuario, string password)
        {
            //validar si mi usuario y mi password estan incluidos en el Login
            //si existe devuelve true si no false
           var valido = _context.Logins.Any( x=>x.Usuario == usuario && x.Password == password);
            return valido;
            
        }
    }
}
