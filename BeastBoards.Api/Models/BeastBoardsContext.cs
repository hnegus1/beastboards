using Microsoft.EntityFrameworkCore;

namespace BeastBoards.Api.Models
{
    public class BeastBoardsContext : DbContext
    {
        public DbSet<LeaderboardTiming> LeaderboardTimings { get; set; }

        public BeastBoardsContext(DbContextOptions<BeastBoardsContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<LeaderboardTiming>().ToTable("BeastBoards_LeaderboardTimings");
        }
    }
}
