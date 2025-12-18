using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    public class Proveedor
    {
        
      
            [Key]
            public int ID_Proveedor { get; set; }

            [Required]
            [StringLength(200)]
            public string Nombre { get; set; }

            [StringLength(100)]
            public string Razon_social { get; set; }

            [StringLength(15)]
            public string Telefono { get; set; }

            [StringLength(200)]
            public string Direccion { get; set; }

            [StringLength(100)]
            public string Correo { get; set; }

            public bool Activo { get; set; } = true;
        }
    }

