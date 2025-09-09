using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace proyectopdf.Models
{
    public partial class DetalleVenta
    {
        [Key]
        public int IdDetalleVenta { get; set; }

        [ForeignKey("Venta")]
        public int? IdVenta { get; set; }
        public string? NombreProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }

        public virtual Venta? Venta { get; set; }

    }
}
