using Microsoft.EntityFrameworkCore;
using VMS2._0.Models;

namespace VMS2._0.Data
{
    public class VMSDbContext : DbContext
    {
        public VMSDbContext(DbContextOptions<VMSDbContext> options) : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SecondaryInfo> SecondaryInfos { get; set; }
        public DbSet<URLManagement> URLManagements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.Visitor)
                .WithMany(v => v.Visits)
                .HasForeignKey(v => v.VisitorID);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Visit)
                .WithMany()
                .HasForeignKey(n => n.VisitID);

            modelBuilder.Entity<SecondaryInfo>()
                .HasOne(s => s.Visitor)
                .WithMany()
                .HasForeignKey(s => s.VisitorID);

            modelBuilder.Entity<URLManagement>()
                .HasOne(u => u.Visit)
                .WithMany()
                .HasForeignKey(u => u.VisitID);
        }
    }
}
