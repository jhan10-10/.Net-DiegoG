// Domicilio.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TostonApp.Ventas.Models
{
    [Table("Entregas")]
    public class Domicilio
    {
        [Key]
        public int ID_entrega { get; set; }

        [Required]
        public int ID_venta { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }

        [Required]
        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(200)]
        public string Ciudad { get; set; }

        [StringLength(100)]
        public string Responsable { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CostoEnvio { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public bool Activo { get; set; } = true;

        // ✅ Navegación - Relación opcional con Venta
        [ForeignKey("ID_venta")]
        public virtual Venta? Venta { get; set; }
    }
}