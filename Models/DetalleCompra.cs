using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Tienda.Models
{
    [Table("DetalleCompra")]
    public partial class DetalleCompra
    {
        [Key]
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "money")]
        public decimal Precio { get; set; }
        [Column("IVA", TypeName = "money")]
        public decimal Iva { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [ForeignKey(nameof(IdCompra))]
        [InverseProperty("DetalleCompras")]
        public virtual Compra Compra { get; set; }
        [ForeignKey(nameof(IdProducto))]
        [InverseProperty("DetalleCompras")]
        public virtual Producto Producto { get; set; }
    }
}
