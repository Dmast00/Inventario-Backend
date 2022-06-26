using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    public class Categorias
    {
        [Key]
        public int IdCategoria { get; set; }
        public string C_Nombre { get; set; }
        public string C_Descripcion { get; set; }
    }
}
