﻿using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        public string P_Nombre { get; set; }
        public virtual int FK_IdCategoria { get; set; }
        public string P_Descripcion { get; set; }
        public decimal P_Precio { get; set; }
    }
}
