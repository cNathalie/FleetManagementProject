using System;
using System.Collections.Generic;
using EF_Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Infrastructure.Context;

public partial class FleetManagementDbContext : DbContext
{
    public FleetManagementDbContext()
    {
    }

    public FleetManagementDbContext(DbContextOptions<FleetManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bestuurder> Bestuurders { get; set; }

    public virtual DbSet<BrandstofType> BrandstofTypes { get; set; }

    public virtual DbSet<Fleet> Fleet { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Tankkaart> Tankkaarten { get; set; }

    public virtual DbSet<TypeRijbewijs> TypeRijbewijs { get; set; }

    public virtual DbSet<TypeWagen> TypeWagens { get; set; }

    public virtual DbSet<Voertuig> Voertuigen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        //VERVANG HIER DE CONNECTION STRING NAAR DIE VAN JOU DATABASE !!!

        => optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=FleetManagementDB;Persist Security Info=True;User ID=sa;TrustServerCertificate=True;Password=FleetManagement007");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bestuurder>(entity =>
        {
            entity.ToTable("Bestuurder");

            entity.Property(e => e.BestuurderId).HasColumnName("bestuurderId");
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
                .HasConstraintName("FK_Bestuurder_TypeRijbewijs");
        });

        modelBuilder.Entity<BrandstofType>(entity =>
        {
            entity.ToTable("BrandstofType");

            entity.Property(e => e.BrandstofTypeId).HasColumnName("brandstofTypeId");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Fleet>(entity =>
        {
            entity.ToTable("Fleet");

            entity.HasIndex(e => e.BestuurderId, "UK_Fleet_B").IsUnique();

            entity.HasIndex(e => new { e.BestuurderId, e.TankkaartId, e.VoertuigId }, "UK_Fleet_BTV").IsUnique();

            entity.HasIndex(e => e.TankkaartId, "UK_Fleet_T").IsUnique();

            entity.HasIndex(e => e.VoertuigId, "UK_Fleet_V").IsUnique();

            entity.Property(e => e.FleetId).HasColumnName("fleetId");
            entity.Property(e => e.BestuurderId).HasColumnName("bestuurderId");
            entity.Property(e => e.TankkaartId).HasColumnName("tankkaartId");
            entity.Property(e => e.VoertuigId).HasColumnName("voertuigId");

            entity.HasOne(d => d.Bestuurder).WithOne(p => p.Fleet)
                .HasForeignKey<Fleet>(d => d.BestuurderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fleet_Bestuurder");

            entity.HasOne(d => d.Tankkaart).WithOne(p => p.Fleet)
                .HasForeignKey<Fleet>(d => d.TankkaartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fleet_Tankkaart");

            entity.HasOne(d => d.Voertuig).WithOne(p => p.Fleet)
                .HasForeignKey<Fleet>(d => d.VoertuigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fleet_Voertuig");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("Login");

            entity.Property(e => e.LoginId).HasColumnName("loginId");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Rol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('User')")
                .HasColumnName("rol");
            entity.Property(e => e.Wachtwoord)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("wachtwoord");
        });

        modelBuilder.Entity<Tankkaart>(entity =>
        {
            entity.ToTable("Tankkaart");

            entity.Property(e => e.TankkaartId).HasColumnName("tankkaartId");
            entity.Property(e => e.Actief)
                .HasDefaultValueSql("((1))")
                .HasColumnName("actief");
            entity.Property(e => e.BrandstofTypeId).HasColumnName("brandstofTypeId");
            entity.Property(e => e.Geldigheidsdatum)
                .HasColumnType("datetime")
                .HasColumnName("geldigheidsdatum");
            entity.Property(e => e.Kaartnummer).HasColumnName("kaartnummer");
            entity.Property(e => e.Pincode).HasColumnName("pincode");

            entity.HasOne(d => d.BrandstofType).WithMany(p => p.Tankkaarts)
                .HasForeignKey(d => d.BrandstofTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tankkaart_BrandstofType");
        });

        modelBuilder.Entity<TypeRijbewijs>(entity =>
        {
            entity.HasKey(e => e.TypeRijbewijsId);

            entity.Property(e => e.TypeRijbewijsId).HasColumnName("typeRijbewijsId");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<TypeWagen>(entity =>
        {
            entity.ToTable("TypeWagen");

            entity.Property(e => e.TypeWagenId).HasColumnName("typeWagenId");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Voertuig>(entity =>
        {
            entity.ToTable("Voertuig");

            entity.Property(e => e.VoertuigId).HasColumnName("voertuigId");
            entity.Property(e => e.AantalDeuren).HasColumnName("aantal_deuren");
            entity.Property(e => e.BrandstofTypeId).HasColumnName("brandstofTypeId");
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
            entity.Property(e => e.TypeWagenId).HasColumnName("typeWagenId");

            entity.HasOne(d => d.BrandstofType).WithMany(p => p.Voertuigs)
                .HasForeignKey(d => d.BrandstofTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voertuig_BrandstofType");

            entity.HasOne(d => d.TypeWagen).WithMany(p => p.Voertuigs)
                .HasForeignKey(d => d.TypeWagenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Voertuig_TypeWagen");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
