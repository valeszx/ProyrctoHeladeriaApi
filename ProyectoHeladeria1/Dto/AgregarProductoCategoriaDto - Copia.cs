using ProyectoHeladeria1.Modelo;

namespace ProyectoHeladeria1.Dto
{
    public class AgregarProductoCategoriaDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public bool Disponible { get; set; } = true;

        public Categoria Categorias { get; set; }

        /// <summary>Ids de los sabores del producto. Solo aplica si la categoria LlevaSabor. En actualizar, null = no cambiar.</summary>
        public List<int>? SaboresIds { get; set; }
    }
}
