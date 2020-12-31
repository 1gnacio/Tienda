using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Tienda.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        [Key]
        public int IdCategoria { get; set; }
        [Required]
        [Column("Categoria")]
        [StringLength(50)]
        public string sCategoria { get; set; }

        [InverseProperty(nameof(Producto.Categoria))]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
