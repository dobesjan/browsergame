using BrowserGame.Models.Resources;
using BrowserGame.Models.Users;
using BrowserGame.Models.Villages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region DbSets
        #region Resources
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceField> ResourceFields { get; set; }
        #endregion

        #region Players
        public DbSet<Player> Players { get; set; }
        #endregion

        #region Village
        public DbSet<Village> Villages { get; set; }
        public DbSet<VillageResource> VillageResources { get; set; }
        public DbSet<VillageResourceField> VillageResourceFields { get; set; }
        #endregion
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VillageResource>()
                .HasKey(pc => new { pc.VillageId, pc.ResourceId });

            modelBuilder.Entity<VillageResource>()
                .HasOne(pc => pc.Village)
                .WithMany(p => p.VillageResources)
                .HasForeignKey(pc => pc.VillageId);

            modelBuilder.Entity<VillageResource>()
                .HasOne(pc => pc.Resource)
                .WithMany(c => c.VillageResources)
                .HasForeignKey(pc => pc.ResourceId);

            modelBuilder.Entity<VillageResourceField>()
                .HasKey(pc => new { pc.VillageId, pc.ResourceFieldId });

            modelBuilder.Entity<VillageResourceField>()
                .HasOne(pc => pc.Village)
                .WithMany(p => p.VillageFields)
                .HasForeignKey(pc => pc.VillageId);

            modelBuilder.Entity<VillageResourceField>()
                .HasOne(pc => pc.ResourceField)
                .WithMany(c => c.VillageFields)
                .HasForeignKey(pc => pc.ResourceFieldId);
        }
    }
}
