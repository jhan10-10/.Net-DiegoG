// Cotizacion.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    [Table("Cotizaciones")]
    public class Cotizacion
    {
        [Key]
        public int ID_Cotizacion { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroCotizacion { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        [Required]
        [StringLength(200)]
        public string Cliente { get; set; }

        [StringLength(100)]
        public string Cedula { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public int ID_Usuario { get; set; }

        public bool Activo { get; set; } = true;
    }
}