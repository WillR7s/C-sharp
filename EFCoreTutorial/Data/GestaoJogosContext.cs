using System;
using System.Collections.Generic;
using EFCoreTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial.Data;

public partial class GestaoJogosContext : DbContext
{
    public GestaoJogosContext(DbContextOptions<GestaoJogosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Desenvolvedora> Desenvolvedoras { get; set; }

    public virtual DbSet<Jogador> Jogadors { get; set; }

    public virtual DbSet<JogadorJogo> JogadorJogos { get; set; }

    public virtual DbSet<Jogo> Jogos { get; set; }

    public virtual DbSet<PerfilJogador> PerfilJogadors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Desenvolvedora>(entity =>
        {
            entity.HasKey(e => e.IdDesenvolvedora).HasName("PK__Desenvol__C26F4F67C15A1400");

            entity.ToTable("Desenvolvedora");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jogador>(entity =>
        {
            entity.HasKey(e => e.IdJogador).HasName("PK__Jogador__94A4447C34836923");

            entity.ToTable("Jogador");

            entity.HasIndex(e => e.Email, "UQ__Jogador__A9D1053428AA1401").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JogadorJogo>(entity =>
        {
            entity.HasKey(e => e.IdJogadorJogo).HasName("PK__JogadorJ__E1F1771EAA8217AC");

            entity.ToTable("JogadorJogo");

            entity.HasIndex(e => new { e.IdJogador, e.IdJogo }, "UQ_JogadorJogo").IsUnique();

            entity.Property(e => e.DataAquisicao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HorasJogadas).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Na biblioteca");

            entity.HasOne(d => d.IdJogadorNavigation).WithMany(p => p.JogadorJogos)
                .HasForeignKey(d => d.IdJogador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JogadorJogo_Jogador");

            entity.HasOne(d => d.IdJogoNavigation).WithMany(p => p.JogadorJogos)
                .HasForeignKey(d => d.IdJogo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JogadorJogo_Jogo");
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.IdJogo).HasName("PK__Jogo__69E08513C3AF63F1");

            entity.ToTable("Jogo");

            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdDesenvolvedoraNavigation).WithMany(p => p.Jogos)
                .HasForeignKey(d => d.IdDesenvolvedora)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Jogo_Desenvolvedora");
        });

        modelBuilder.Entity<PerfilJogador>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__PerfilJo__C7BD5CC15F8BAE28");

            entity.ToTable("PerfilJogador");

            entity.HasIndex(e => e.IdJogador, "UQ__PerfilJo__94A4447D960A872F").IsUnique();

            entity.HasIndex(e => e.Nickname, "UQ__PerfilJo__CC6CD17E5FA9C8AB").IsUnique();

            entity.Property(e => e.Biografia)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Plataforma)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdJogadorNavigation).WithOne(p => p.PerfilJogador)
                .HasForeignKey<PerfilJogador>(d => d.IdJogador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilJogador_Jogador");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
