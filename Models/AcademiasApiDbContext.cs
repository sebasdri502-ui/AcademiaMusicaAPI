using Microsoft.EntityFrameworkCore;

namespace AcademiaMusicaAPI.Models
{
    public class AcademiasApiDbContext : DbContext
    {
        public AcademiasApiDbContext(DbContextOptions<AcademiasApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Estudiantes");

                // LE DECIMOS A EF QUE LA BASE DE DATOS GENERA ESTE VALOR SOLO
                entity.Property(e => e.FechaInscripcion)
                      .HasDefaultValueSql("(getdate())")
                      .ValueGeneratedOnAdd();
            });
        }
    }
}