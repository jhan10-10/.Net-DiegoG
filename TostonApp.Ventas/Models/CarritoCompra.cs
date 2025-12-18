using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    [Table("CarritoCompra")]
    public class CarritoCompra
    {
        [Key]
        public int ID_Carrito { get; set; }

        // ✅ NUEVO: Relación con Usuario (nullable para permitir carritos anónimos)
        public int? ID_Usuario { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un producto")]
        public int ID_Producto { get; set; }

        [Required, Range(1, 999999, ErrorMessage = "Cantidad inválida")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; } = 1;

        [ValidateNever]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [ValidateNever]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [Display(Name = "Fecha Agregado")]
        public DateTime FechaAgregado { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;

        // Navegación
        [ValidateNever]
        public Usuario? Usuario { get; set; }

        [ValidateNever]
        public Producto? Producto { get; set; }
    }
}