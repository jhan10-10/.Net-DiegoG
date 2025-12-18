using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TostonApp.Ventas.Models
{
    public class Usuario
    {
        [Key]
        public int ID_Usuario { get; set; }

        [Required]
        public string Cedula { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        public string Correo_Electronico { get; set; }

        public bool Activo { get; set; } = true;

        // 🔹 NUEVO
        public List<Pedido> Pedidos { get; set; } = new();
    }

}

