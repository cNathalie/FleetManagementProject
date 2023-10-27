using System;
using System.Collections.Generic;
using EFInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EFInfrastructure.Context;

public partial class FleetManagementContext : DbContext
{
    public FleetManagementContext()
    {
    }

    public FleetManagementContext(DbContextOptions<FleetManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bestuurder> Bestuurders { get; set; }

    public virtual DbSet<BrantstofType> BrantstofTypes { get; set; }

    public virtual DbSet<Fleet> Fleets { get; set; }

    public virtual DbSet<Tankkaarten> Tankkaartens { get; set; }

    public virtual DbSet<TypeRijbewij> TypeRijbewijs { get; set; }

    public virtual DbSet<TypeWagen> TypeWagens { get; set; }

    public virtual DbSet<Voertuig> Voertuigs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=FleetManagement;Persist Security Info=True;User ID=sa;TrustServerCertificate=True;Password=FleetManagement007");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bestuurder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bestuurd__3213E83F7C4EC7E6");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("adres");
            entity.Property(e => e.Naam)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("naam");
            entity.Property(e => e.Rijksregisternummer)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("rijksregisternummer");
            entity.Property(e => e.TyperijbewijsId).HasColumnName("typerijbewijsId");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("voornaam");

            entity.HasOne(d => d.Typerijbewijs).WithMany(p => p.Bestuurders)
                .HasForeignKey(d => d.TyperijbewijsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Bestuurders_fk0");
        });

        modelBuilder.Entity<BrantstofType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brantsto__3213E83F0014E1D6");

            entity.ToTable("BrantstofType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Fleet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fleet__3213E83F30EBE2F0");

            entity.ToTable("Fleet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BestuurderId).HasColumnName("bestuurderId");
            entity.Property(e => e.TankkaartId).HasColumnName("tankkaartId");
            entity.Property(e => e.VoertuigId).HasColumnName("voertuigId");

            entity.HasOne(d => d.Bestuurder).WithMany(p => p.Fleets)
                .HasForeignKey(d => d.BestuurderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fleet_fk0");

            entity.HasOne(d => d.Tankkaart).WithMany(p => p.Fleets)
                .HasForeignKey(d => d.TankkaartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fleet_fk1");

            entity.HasOne(d => d.Voertuig).WithMany(p => p.Fleets)
                .HasForeignKey(d => d.VoertuigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fleet_fk2");
        });

        modelBuilder.Entity<Tankkaarten>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tankkaar__3213E83F169D3FC6");

            entity.ToTable("Tankkaarten");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actief)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("actief");
            entity.Property(e => e.Brandstoffen)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("brandstoffen");
            entity.Property(e => e.Geldigheidsdatum)
                .HasColumnType("datetime")
                .HasColumnName("geldigheidsdatum");
            entity.Property(e => e.Kaartnummer).HasColumnName("kaartnummer");
            entity.Property(e => e.Pincode).HasColumnName("pincode");
        });

        modelBuilder.Entity<TypeRijbewij>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypeRijb__3213E83F71DB6A8E");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<TypeWagen>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypeWage__3213E83F6CC176D4");

            entity.ToTable("TypeWagen");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Voertuig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voertuig__3213E83F46D5D225");

            entity.ToTable("Voertuig");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AantalDeuren).HasColumnName("aantal_deuren");
            entity.Property(e => e.BrandstoftypeId).HasColumnName("brandstoftypeId");
            entity.Property(e => e.Chassisnummer)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("chassisnummer");
            entity.Property(e => e.Kleur)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("kleur");
            entity.Property(e => e.MerkEnModel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("merkEnModel");
            entity.Property(e => e.Nummerplaat)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nummerplaat");
            entity.Property(e => e.TypewagenId).HasColumnName("typewagenId");

            entity.HasOne(d => d.Brandstoftype).WithMany(p => p.Voertuigs)
                .HasForeignKey(d => d.BrandstoftypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Voertuig_fk0");

            entity.HasOne(d => d.Typewagen).WithMany(p => p.Voertuigs)
                .HasForeignKey(d => d.TypewagenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Voertuig_fk1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
