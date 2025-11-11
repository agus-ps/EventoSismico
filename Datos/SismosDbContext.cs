using EventoSismicoApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventoSismicoApp.Datos
{
    public class SismosDbContext : DbContext
    {
        // 1. Declaramos una tabla (DbSet) para CADA entidad que queremos guardar
        public DbSet<EventoSismico> EventosSismicos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<SerieTemporal> SeriesTemporales { get; set; }
        public DbSet<MuestraSismica> MuestrasSismicas { get; set; }
        public DbSet<DetalleMuestraSismica> DetallesMuestrasSismicas { get; set; }
        public DbSet<Sismografo> Sismografos { get; set; }
        public DbSet<EstacionSismologica> EstacionesSismologicas { get; set; }
        public DbSet<TipoDato> TiposDeDato { get; set; }
        public DbSet<AlcanceSismo> AlcancesSismos { get; set; }
        public DbSet<OrigenDeGeneracion> OrigenesDeGeneracion { get; set; }
        public DbSet<ClasificacionSismo> ClasificacionesSismos { get; set; }
        public DbSet<CambioEstado> CambiosDeEstado { get; set; }


        // 2. Le decimos a EF Core dónde guardar el archivo de la DB
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Esto creará un archivo "sismos.db" en la carpeta de tu ejecutable
            optionsBuilder.UseSqlite("Data Source=sismos.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Le decimos a EF Core cómo armar la relación 1-a-1
            modelBuilder.Entity<Usuario>()
                .HasOne(usuario => usuario.Empleado) // Un Usuario tiene un Empleado
                .WithOne(empleado => empleado.Usuario) // Un Empleado tiene un Usuario
                .HasForeignKey<Usuario>(usuario => usuario.EmpleadoId); // La llave foránea 'EmpleadoId' está en la tabla 'Usuario'
        }
    }
}