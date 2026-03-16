using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SysSeguridad2G05.EN
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(30, ErrorMessage="Maximo 30 caracteres")]
        public string? Nombre { get; set; }
        public List<Usuario> Usuarios { get; set; } = null!;

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
