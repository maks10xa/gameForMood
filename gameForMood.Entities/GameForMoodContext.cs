using Microsoft.EntityFrameworkCore;

namespace gameForMood.Entities;

public partial class GameForMoodContext : DbContext
{
    public GameForMoodContext()
    {
    }

    public GameForMoodContext(DbContextOptions<GameForMoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Info> Infos { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGameRelation> UserGameRelations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contacts_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Logo).HasColumnType("character varying");
            entity.Property(e => e.Value).HasColumnType("character varying");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Image).HasColumnType("character varying");
            entity.Property(e => e.Info)
                .HasColumnType("character varying")
                .HasColumnName("info");
            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.HasOne(d => d.Genre).WithMany(p => p.Games)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("games_genres_fk");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Info>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("info_pk");

            entity.ToTable("Info");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Location).HasColumnType("character varying");
            entity.Property(e => e.Title).HasColumnType("character varying");
            entity.Property(e => e.Value).HasColumnType("character varying");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("images_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Image).HasColumnType("character varying");
            entity.Property(e => e.Location).HasColumnType("character varying");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prices_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Price1)
                .HasColumnType("character varying")
                .HasColumnName("Price");
            entity.Property(e => e.Value).HasColumnType("character varying");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.Image).HasColumnType("character varying");
            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Username).HasColumnType("character varying");
        });

        modelBuilder.Entity<UserGameRelation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usergamerelations_pk");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
