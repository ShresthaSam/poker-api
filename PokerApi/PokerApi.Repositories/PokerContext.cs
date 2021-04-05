using Microsoft.EntityFrameworkCore;
using PokerApi.Common;
using PokerApi.Repositories.Configurations;
using PokerApi.Repositories.Entities;
using Serilog;
using System.Linq;

namespace PokerApi.Repositories
{
    public class PokerContext : DbContext
    {
        private readonly ILogger _logger;

        public DbSet<Card> Cards { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Hand> Hands { get; set; }

        public PokerContext() { }

        public PokerContext(
            ILogger logger)
        {
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var binDirDbPath = GetAbsoluteDBPathForSqlite();
            _logger?.Information("Started configuring SQLite Database DBFullPath:{DBFullPath}", binDirDbPath);
            optionsBuilder.UseSqlite($"Data Source={binDirDbPath};");

            _logger?.Information("Finished configuring SqlLite database");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _logger?.Information("Started configuring OnModelCreating");

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CardEntityConfig).Assembly);

            modelBuilder.Entity<Card>().HasData(
                CardDataStore.GetAllCardItems()
                    .Select(i => new Card { CardId = i.Id, Suit = i.Suit, Number = i.Number })
                    .ToList());
            _logger?.Information("Seeded card data");

            _logger?.Information("Finished configuring OnModelCreating");
        }

        private string GetAbsoluteDBPathForSqlite()
        {
            var path = $"../{Constants.POKER_DB_NAME_SQLITE}";
            return path;
        }
    }
}
