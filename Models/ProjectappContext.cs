using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal_APP.Models;

public partial class ProjectappContext : DbContext
{
    public ProjectappContext()
    {
    }

    public ProjectappContext(DbContextOptions<ProjectappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__REGISTRO__5B65BF97B948C269");

            entity.ToTable("REGISTRO");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Edad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PesoCorporal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Peso_Corporal");
            entity.Property(e => e.PlanEntreno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Plan_Entreno");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__RESERVAS__B7C92638EAE49940");

            entity.ToTable("RESERVAS");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Registro");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Reserva1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Reserva");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("USUARIOS");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
