using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    public class Insumo
    {
       
            [Key]
            public int ID_Insumo { get; set; }

            [Required]
            [StringLength(200)]
            public string Nombre { get; set; }

            public int? ID_Categoria { get; set; }

            [StringLength(100)]
            public string Estado_Insumo { get; set; }

            public int? Stock_Actual { get; set; }

            public int? Stock_Minimo { get; set; }

            public int? ID_Lote_Insu { get; set; }

            public bool Activo { get; set; } = true;
        }

    }

