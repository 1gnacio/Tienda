using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Tienda.Models
{
    public partial class DetalleVentum
    {
        [Key]
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "money")]
        public decimal Precio { get; set; }
        [Column("IVA", TypeName = "money")]
        public decimal Iva { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [ForeignKey(nameof(IdProducto))]
        [InverseProperty("DetalleVenta")]
        public virtual Producto Producto { get; set; }
        [ForeignKey(nameof(IdVenta))]
        [InverseProperty("DetalleVenta")]
        public virtual Venta Venta { get; set; }
    }
}
