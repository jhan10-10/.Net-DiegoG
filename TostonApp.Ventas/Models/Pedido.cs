using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int ID_Pedido { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        // ✅ NUEVO: Relación con Usuario/Cliente
        public int? ID_Usuario { get; set; }

        [StringLength(100)]
        [Display(Name = "Cédula")]
        public string? Cedula { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required, StringLength(50)]
        public string Estado { get; set; } = "Pendiente"; // Pendiente / En Proceso / Completado / Cancelado

        [StringLength(500)]
        public string? Observaciones { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // ✅ Navegación
        [ValidateNever]
        public Usuario? Usuario { get; set; }

        [ValidateNever]
        public List<PedidoDetalle> Detalles { get; set; } = new();
    }
}