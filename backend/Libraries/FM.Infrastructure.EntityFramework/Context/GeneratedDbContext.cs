using System;
using System.Collections.Generic;
using FM.Infrastructure.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FM.Infrastructure.EntityFramework.Context;

public partial class GeneratedDbContext : DbContext
{
    public GeneratedDbContext()
    {
    }

    public GeneratedDbContext(DbContextOptions<GeneratedDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bestuurder> Bestuurders { get; set; }

    public virtual DbSet<BrandstofType> BrandstofTypes { get; set; }

    public virtual DbSet<FleetMember> FleetMembers { get; set; }

    public virtual DbSet<Tankkaart> Tankkaarten { get; set; }

    public virtual DbSet<TypeRijbewijs> TypeRijbewijs { get; set; }

    public virtual DbSet<TypeWagen> TypeWagens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    public virtual DbSet<Voertuig> Voertuigen { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bestuurder>(entity =>
        {
            entity.ToTable("Bestuurder", tb => tb.HasTrigger("TRG_Bestuurder_Update"));

            entity.Property(e => e.BestuurderId).HasColumnName("bestuurderId");
            entity.Property(e => e.Adres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("adres");
            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
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
            entity.ToTable("BrandstofType", tb => tb.HasTrigger("TRG_BrandstofType_Update"));

            entity.Property(e => e.BrandstofTypeId).HasColumnName("brandstofTypeId");
            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<FleetMember>(entity =>
        {
            entity.HasKey(e => e.FleetId).HasName("PK_Fleet");

            entity.ToTable("FleetMember", tb => tb.HasTrigger("TRG_FleetMember_Update"));

            entity.HasIndex(e => new { e.BestuurderId, e.TankkaartId, e.VoertuigId }, "IX_Fleet_UniqueCombinations")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.FleetId).HasColumnName("fleetId");
            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
            entity.Property(e => e.BestuurderId).HasColumnName("bestuurderId");
            entity.Property(e => e.TankkaartId).HasColumnName("tankkaartId");
            entity.Property(e => e.VoertuigId).HasColumnName("voertuigId");

            entity.HasOne(d => d.Bestuurder).WithMany(p => p.FleetMembers)
                .HasForeignKey(d => d.BestuurderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fleet_Bestuurder");

            entity.HasOne(d => d.Tankkaart).WithMany(p => p.FleetMembers)
                .HasForeignKey(d => d.TankkaartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fleet_Tankkaart");

            entity.HasOne(d => d.Voertuig).WithMany(p => p.FleetMembers)
                .HasForeignKey(d => d.VoertuigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fleet_Voertuig");
        });

        modelBuilder.Entity<Tankkaart>(entity =>
        {
            entity.ToTable("Tankkaart", tb => tb.HasTrigger("TRG_Tankkaart_Update"));

            entity.HasIndex(e => e.Kaartnummer, "UQ_Tankaart_Kaartnummer").IsUnique();

            entity.Property(e => e.TankkaartId).HasColumnName("tankkaartId");
            entity.Property(e => e.Actief)
                .HasDefaultValueSql("((1))")
                .HasColumnName("actief");
            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
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

            entity.ToTable(tb => tb.HasTrigger("TRG_TypeRijbewijs_Update"));

            entity.Property(e => e.TypeRijbewijsId).HasColumnName("typeRijbewijsId");
            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<TypeWagen>(entity =>
        {
            entity.ToTable("TypeWagen", tb => tb.HasTrigger("TRG_TypeWagen_Update"));

            entity.Property(e => e.TypeWagenId).HasColumnName("typeWagenId");
            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Login");

            entity.ToTable("User", tb => tb.HasTrigger("TRG_User_Update"));

            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).IsUnicode(false);
            entity.Property(e => e.PasswordSalt).IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('User')");
        });

        modelBuilder.Entity<UserRefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK_RefreshToken");

            entity.ToTable("UserRefreshToken", tb => tb.HasTrigger("TRG_UserRefreshToken_Update"));

            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.RefreshToken).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserRefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_User");
        });

        modelBuilder.Entity<Voertuig>(entity =>
        {
            entity.ToTable("Voertuig", tb => tb.HasTrigger("TRG_Voertuig_Update"));

            entity.HasIndex(e => e.Chassisnummer, "FK_Voertuig_Chassisnummer").IsUnique();

            entity.HasIndex(e => e.Nummerplaat, "FK_Voertuig_Nummerplaat").IsUnique();

            entity.Property(e => e.VoertuigId).HasColumnName("voertuigId");
            entity.Property(e => e.AantalDeuren).HasColumnName("aantal_deuren");
            entity.Property(e => e.AutoTimeCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_CREATION");
            entity.Property(e => e.AutoTimeUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("AUTO_TIME_UPDATE");
            entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");
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
