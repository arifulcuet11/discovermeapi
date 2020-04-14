
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Domain.Auth;
using WebApp.Domain.Entities;
using WebApp.Domain.VM;

namespace WebApp.Infrastructure.Context
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ContentViewMobile>().HasNoKey();
        }

        //public DbSet<Test> Tests { get; set; }
        public DbSet<WebAddress> WebAddresses { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<GenderType> GenderTypes { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ContentLike> ContentLikes { get; set; }
        public DbSet<MarkAsRead> MarkAsReads { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<MobileCoverImage> MobileCoverImages { get; set; }

    }
}
