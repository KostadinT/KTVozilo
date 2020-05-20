using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VoziloKT.Models
{
    //This creates DB Context which is corresponded to the created elements in the VoziloDB database (tables, etc...)
    //For the creation of this DBContext is used EntityFramework.Core
    public partial class voziloDBContext : DbContext
    {
        public voziloDBContext()
        {
        }

        public voziloDBContext(DbContextOptions<voziloDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataForVehicles> DataForVehicles { get; set; }
        public virtual DbSet<MarkaNaVozilo> MarkaNaVozilo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=VoziloDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataForVehicles>(entity =>
            {
                entity.HasIndex(e => e.LicensePlateNumber)
                    .HasName("UQ__DataForV__E78327C610547C34")
                    .IsUnique();

                entity.HasIndex(e => e.Vin)
                    .HasName("UQ__DataForV__C5DF234C6A6E42E1")
                    .IsUnique();

                entity.Property(e => e.LicensePlateNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleModel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vin)
                    .IsRequired()
                    .HasColumnName("VIN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.VehicleModelNavigation)
                    .WithMany(p => p.DataForVehicles)
                    .HasPrincipalKey(p => p.VehicleModel)
                    .HasForeignKey(d => d.VehicleModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DataForVe__Vehic__3D5E1FD2");
            });

            modelBuilder.Entity<MarkaNaVozilo>(entity =>
            {
                entity.HasIndex(e => e.VehicleModel)
                    .HasName("UQ__MarkaNaV__D944058D7A71D209")
                    .IsUnique();

                entity.Property(e => e.VehicleModel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
