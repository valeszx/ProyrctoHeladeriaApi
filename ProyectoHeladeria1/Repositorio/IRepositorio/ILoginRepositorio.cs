using Proyecto_Heladeria.Modelo;

namespace Proyecto_Heladeria.Repositorio.IRepositorio
{
    public interface ILoginRepositorio
    {
        //agregar los nombres de los metodos para el login
        ICollection<Login> GetLogins();
        bool ValidarLogin(string usuario, string password);
    }
}
