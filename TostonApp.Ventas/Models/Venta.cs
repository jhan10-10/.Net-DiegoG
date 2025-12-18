using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    [Table("Ventas")]
    public class Venta
    {
        [Key]
        public int ID_venta { get; set; }

        public int ID_Pedido { get; set; }

        [ValidateNever]
        public Pedido? Pedido { get; set; }

        public DateTime Fecha_Venta { get; set; } = DateTime.Now;

        [ValidateNever]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un método de pago")]
        [StringLength(50)]
        public string MetodoPago { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string Estado { get; set; } = "Pendiente"; // Pendiente / Completada / Cancelada

        [StringLength(500)]
        public string? Observaciones { get; set; }

        public bool Activo { get; set; } = true;
    }
}
