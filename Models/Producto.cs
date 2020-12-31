using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Tienda.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleCompras = new HashSet<DetalleCompra>();
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        [Key]
        public int IdProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(50)]
        public string Marca { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        
        [Required]
        public int IdCategoria { get; set; }

        [ForeignKey(nameof(IdCategoria))]
        [InverseProperty("Productos")]
        public virtual Categoria Categoria { get; set; }
        [InverseProperty(nameof(DetalleCompra.Producto))]
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        [InverseProperty(nameof(DetalleVentum.Producto))]
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
