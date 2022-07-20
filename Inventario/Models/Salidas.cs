using System;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    public class Salidas
    {
        [Key]
        public int IdSalida { get; set; }
        public decimal S_Total { get; set; }
        public DateTime S_Fecha { get; set; }
    }
}
