using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaASPNet.Models
{
    [Table("Users")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("Username")]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Column("Password")]
        [MaxLength(256)]
        public string Password { get; set; } = string.Empty;

        [Column("Nombres")]
        [MaxLength(200)]
        public string Nombres { get; set; } = string.Empty;

        [Column("PrimerApellido")]
        [MaxLength(200)]
        public string PrimerApellido { get; set; } = string.Empty;

        [Column("SegundoApellido")]
        [MaxLength(200)]
        public string SegundoApellido { get; set; } = string.Empty;

        [Column("TipoDocumento")]
        [MaxLength(50)]
        public string TipoDocumento { get; set; } = string.Empty;

        [Column("NumeroDocumento")]
        [MaxLength(50)]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Column("FechaNacimiento", TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        [Column("Nacionalidad")]
        [MaxLength(100)]
        public string Nacionalidad { get; set; } = string.Empty;

        [Column("Sexo")]
        [MaxLength(50)]
        public string Sexo { get; set; } = string.Empty;

        [Column("CorreoPrincipal")]
        [MaxLength(256)]
        public string CorreoPrincipal { get; set; } = string.Empty;

        [Column("CorreoSecundario")]
        [MaxLength(256)]
        public string? CorreoSecundario { get; set; }

        [Column("TelefonoMovil")]
        [MaxLength(50)]
        public string? TelefonoMovil { get; set; }

        [Column("TipoComunicacion")]
        [MaxLength(100)]
        public string? TipoComunicacion { get; set; }

        [Column("FechaContratacion", TypeName = "date")]
        public DateTime? FechaContratacion { get; set; }

        [Column("Rol")]
        [MaxLength(100)]
        public string Rol { get; set; } = string.Empty;

        [Column("Ubicacion")]
        [MaxLength(200)]
        public string? Ubicacion { get; set; }

        [Column("Estado")]
        [MaxLength(50)]
        public string Estado { get; set; } = "Activo";

        [Column("IntentosFallidos")]
        public int IntentosFallidos { get; set; } = 0;

        [NotMapped]
        public string NombreCompleto => $"{Nombres} {PrimerApellido} {SegundoApellido}".Trim();
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        public string Password { get; set; } = string.Empty;

        public string TipoDocumento { get; set; } = "DNI";
    }
}