using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EasyPoker_API.Models;

public partial class EasyPokerContext : DbContext
{
    public EasyPokerContext()
    {
    }

    public EasyPokerContext(DbContextOptions<EasyPokerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerHand> PlayerHands { get; set; }

    public virtual DbSet<RemainingCard> RemainingCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=EasyPoker;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3214EC073CB3E97B");

            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Winner).HasMaxLength(255);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Players__3214EC07C4E2E9F4");

            entity.Property(e => e.PlayerName).HasMaxLength(255);

            entity.HasOne(d => d.Game).WithMany(p => p.Players)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Players__GameId__3A81B327");
        });

        modelBuilder.Entity<PlayerHand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PlayerHa__3214EC07A148B41B");

            entity.Property(e => e.Card1).HasMaxLength(10);
            entity.Property(e => e.Card2).HasMaxLength(10);
            entity.Property(e => e.Card3).HasMaxLength(10);
            entity.Property(e => e.Card4).HasMaxLength(10);
            entity.Property(e => e.Card5).HasMaxLength(10);

            entity.HasOne(d => d.Game).WithMany(p => p.PlayerHands)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__PlayerHan__GameI__412EB0B6");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerHands)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__PlayerHan__Playe__4222D4EF");
        });

        modelBuilder.Entity<RemainingCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Remainin__3214EC079F8CAE2F");

            entity.Property(e => e.Card).HasMaxLength(10);

            entity.HasOne(d => d.Game).WithMany(p => p.RemainingCards)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Remaining__GameI__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
