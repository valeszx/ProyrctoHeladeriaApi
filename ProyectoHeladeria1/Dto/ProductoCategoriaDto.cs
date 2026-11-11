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

        public bool Disponible { get; set; }

        /// <summary>true si alguna categoria del producto lleva sabor.</summary>
        public bool LlevaSabor { get; set; }

        public List<Sabor> Sabores { get; set; } = new();
    }
}
