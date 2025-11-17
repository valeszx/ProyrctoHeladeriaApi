using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Dto
{
    public class ProductoCategoriaDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public List<Categoria> Categorias { get; set; }
    }
}
