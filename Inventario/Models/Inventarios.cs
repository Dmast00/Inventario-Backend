using System;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    public class Inventarios
    {
        [Key]
        public int IdInventario { get; set; }
        public virtual int FK_IdProducto { get; set; }
        public int P_Existencia { get; set; }
        public int P_InventarioMinimo { get; set; }
        public  DateTime FechaModificacion { get; set; }
    }
}

