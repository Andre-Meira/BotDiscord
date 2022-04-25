using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBaseApplication.Models
{
    public partial class DiscordBotAplicationContext : DbContext
    {
        public DiscordBotAplicationContext()
        {
        }

        public DiscordBotAplicationContext(DbContextOptions<DiscordBotAplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Discordserver> Discordservers { get; set; }
        public virtual DbSet<LogCommand> LogCommands { get; set; }
        public virtual DbSet<RelStreamerXDiscordserf> RelStreamerXDiscordserves { get; set; }
        public virtual DbSet<Streamerdisc> Streamerdiscs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discordserver>(entity =>
            {
                entity.HasKey(e => e.IdServer)
                    .HasName("discordservers_pkey");

                entity.ToTable("discordservers");

                entity.Property(e => e.IdServer)
                    .ValueGeneratedNever()
                    .HasColumnName("id_server");

                entity.Property(e => e.IdChanel).HasColumnName("id_chanel");

                entity.Property(e => e.NameChannel)
                    .HasMaxLength(30)
                    .HasColumnName("name_channel");

                entity.Property(e => e.NameServer)
                    .HasMaxLength(30)
                    .HasColumnName("name_server");
            });

            modelBuilder.Entity<LogCommand>(entity =>
            {
                entity.ToTable("log_commands");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Command)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("command");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.ErrorCommands).HasColumnName("error_commands");

                entity.Property(e => e.IdChannel).HasColumnName("id_channel");

                entity.Property(e => e.IdServer).HasColumnName("id_server");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<RelStreamerXDiscordserf>(entity =>
            {
                entity.HasKey(e => e.IdRel)
                    .HasName("rel_streamer_x_discordserves_pkey");

                entity.ToTable("rel_streamer_x_discordserves");

                entity.Property(e => e.IdRel).HasColumnName("id_rel");

                entity.Property(e => e.FkIdServe).HasColumnName("fk_id_serve");

                entity.Property(e => e.FkStreamer)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fk_streamer");

                entity.HasOne(d => d.FkIdServeNavigation)
                    .WithMany(p => p.RelStreamerXDiscordserves)
                    .HasForeignKey(d => d.FkIdServe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_streamer_x_discordserves_fk_id_serve_fkey");

                entity.HasOne(d => d.FkStreamerNavigation)
                    .WithMany(p => p.RelStreamerXDiscordserves)
                    .HasForeignKey(d => d.FkStreamer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_streamer_x_discordserves_fk_streamer_fkey");
            });

            modelBuilder.Entity<Streamerdisc>(entity =>
            {
                entity.HasKey(e => e.IdStreamer)
                    .HasName("streamerdisc_pkey");

                entity.ToTable("streamerdisc");

                entity.Property(e => e.IdStreamer).HasColumnName("id_streamer");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(30)
                    .HasColumnName("nickname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
