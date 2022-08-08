using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Models;

namespace TwitterWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<TweetLike> TweetLikes { get; set; }
        public DbSet<TweetComment> TweetComments { get; set; }
    }
}
