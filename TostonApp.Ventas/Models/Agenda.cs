// Agenda.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    [Table("Agenda")]
    public class Agenda
    {
        [Key]
        public int ID_agenda { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(10)]
        public string Hora { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(100)]
        public string TipoEvento { get; set; }

        public int? ID_Cliente { get; set; }

        public int ID_Usuario { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
    }
}