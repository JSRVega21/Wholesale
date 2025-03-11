using Wholesale.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Wholesale.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region User
        public DbSet<User> UserWholesale { get; set; }
        #endregion

        #region Visits
        public DbSet<VisitHeader> VisitHeaders { get; set; }
        public DbSet<VisitDetail> VisitDetails { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .OwnsOne(p => p.RecordLog);

            builder.Entity<VisitHeader>()
                .OwnsOne(p => p.RecordLog);

            builder.Entity<VisitDetail>()
                .OwnsOne(p => p.RecordLog);

            // Opcional: asegurar el cascade delete
            builder.Entity<VisitDetail>()
                .HasOne(d => d.VisitHeader)
                .WithMany(h => h.Details)
                .HasForeignKey(d => d.VisitHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
