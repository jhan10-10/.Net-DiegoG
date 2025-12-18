// Devolucion.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    [Table("Devoluciones")]
    public class Devolucion
    {
        [Key]
        public int ID_devolucion { get; set; }

        [Required]
        public int ID_venta { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(100)]
        public string Motivo { get; set; }

        [Required]
        public int ID_Producto { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoDevolucion { get; set; }

        public bool Activo { get; set; } = true;

        // Navegación
        [ForeignKey("ID_venta")]
        public virtual Venta Venta { get; set; }
    }
}
