using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    [Table("PedidoDetalles")]
    public class PedidoDetalle
    {
        [Key]
        public int ID_PedidoDetalle { get; set; }

        [Required]
        public int ID_Pedido { get; set; }

        [Required]
        public int ID_Producto { get; set; }

        [Required, Range(1, 999999)]
        public int Cantidad { get; set; }

        [ValidateNever]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [ValidateNever]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [ValidateNever]
        public Pedido? Pedido { get; set; }

        [ValidateNever]
        public Producto? Producto { get; set; }
    }
}
