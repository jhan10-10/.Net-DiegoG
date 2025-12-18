using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    public class Compra
    {
        
        
            [Key]
            [Column("id_compra")]
            public int ID_Compra { get; set; }

            public int ID_Proveedor { get; set; }

            [Required]
            public DateTime Fecha_Compra { get; set; }

            [StringLength(50)]
            public string Estado { get; set; }

            [Column(TypeName = "decimal(18,2)")]
            public decimal Total_Compra { get; set; }

            public bool Activo { get; set; } = true;

            // Navegación
            [ForeignKey("ID_Proveedor")]
            public virtual Proveedor Proveedor { get; set; }
        }
    }



