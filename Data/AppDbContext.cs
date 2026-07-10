using Microsoft.EntityFrameworkCore;
using PruebaASPNet.Models;

namespace PruebaASPNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Nombres).HasMaxLength(200);
                entity.Property(e => e.PrimerApellido).HasMaxLength(200);
                entity.Property(e => e.SegundoApellido).HasMaxLength(200);
                entity.Property(e => e.TipoDocumento).HasMaxLength(50);
                entity.Property(e => e.NumeroDocumento).HasMaxLength(50);
                entity.Property(e => e.Nacionalidad).HasMaxLength(100);
                entity.Property(e => e.Sexo).HasMaxLength(50);
                entity.Property(e => e.CorreoPrincipal).HasMaxLength(256);
                entity.Property(e => e.CorreoSecundario).HasMaxLength(256);
                entity.Property(e => e.TelefonoMovil).HasMaxLength(50);
                entity.Property(e => e.TipoComunicacion).HasMaxLength(100);
                entity.Property(e => e.Rol).HasMaxLength(100);
                entity.Property(e => e.Ubicacion).HasMaxLength(200);
                entity.Property(e => e.Estado).HasMaxLength(50);
            });

            // Seed data
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Username = "admin",
                Password = "123456",
                Nombres = "July Camila",
                PrimerApellido = "Mendoza",
                SegundoApellido = "Quispe",
                TipoDocumento = "DNI",
                NumeroDocumento = "12345678",
                FechaNacimiento = new DateTime(1995, 5, 15),
                Nacionalidad = "Peruana",
                Sexo = "Femenino",
                CorreoPrincipal = "july.mendoza@ejemplo.gob.pe",
                CorreoSecundario = "july.mq@gmail.com",
                TelefonoMovil = "987654321",
                TipoComunicacion = "Celular",
                FechaContratacion = new DateTime(2020, 3, 1),
                Rol = "Administrador",
                Ubicacion = "Lima, Perú",
                Estado = "Activo",
                IntentosFallidos = 0
            });
        }
    }
}