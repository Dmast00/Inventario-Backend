using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    public class DetalleSalidas
    {
        [Key]
        public int IdDetalle { get; set; }
        public int FK_IdSalida { get; set; }
        public int FK_IdProducto { get; set; }
        public string D_Cantidad { get; set; }
    }
}
