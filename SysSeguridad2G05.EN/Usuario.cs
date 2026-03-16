using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace SysSeguridad2G05.EN
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        [ForeignKey("Rol")]
        [Required(ErrorMessage = "El rol es obligatorio.")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(40, ErrorMessage ="Maximo 40 caracteres")]
        [Display(Name = "Nombre Usuario")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(40, ErrorMessage = "Maximo 40 caracteres")]
        [Display(Name = "Apellido Usuario")]
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "Login de usuario es obligatorio.")]
        [StringLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Password obligatorio.")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio.")]

        public byte status { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        public Rol? Rol { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirmar password es obligatorio.")]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar Contraseña")]
        public string? ConfirmPassword_aux { get; set; }
    }
    public enum Status_Usuario
    {
        Activo = 1,
        Inactivo = 2
    }   

}
