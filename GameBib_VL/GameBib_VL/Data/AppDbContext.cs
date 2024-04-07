using Microsoft.EntityFrameworkCore;
using GameBib_VL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBib_VL.Data
{
    internal class AppDbContext : DbContext
    {
        //Set classes
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteGame> FavoriteGames { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }

        //Connect Database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;" +
                "port=3306;" +
                "user=c_sharp;" +
                "password=Krijnisleider;" +
                "database=Db_GameBib",
                ServerVersion.Parse("8.0.30")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteGame>()
                .HasKey(fg => fg.Id);

            modelBuilder.Entity<FavoriteGame>()
                .HasOne(fg => fg.User)
                .WithMany(u => u.FavoriteGames)
                .HasForeignKey(fg => fg.UserId);

            modelBuilder.Entity<FavoriteGame>()
                .HasOne(fg => fg.Game)
                .WithMany(g => g.LikedByUsers)
                .HasForeignKey(fg => fg.GameId);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Genres)
                .WithMany(g => g.Games)
                .UsingEntity<GameGenre>();

            modelBuilder.Entity<Genre>().HasData(
               new Genre { Id = 1, Name = "Horror" },
               new Genre { Id = 2, Name = "Adventure" },
               new Genre { Id = 3, Name = "Fantasy" }
           );
        }
    }
}
