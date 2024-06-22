using BrowserGame.Models.Buildings;
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
        public DbSet<Effect> Effects { get; set; }
        #endregion

        #region Players
        public DbSet<Player> Players { get; set; }
        #endregion

        #region Village
        public DbSet<BuildQueueItem> BuildQueueItems { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<VillageBuilding> VillageBuildings { get; set; }
        public DbSet<VillageResource> VillageResources { get; set; }
        public DbSet<VillageResourceField> VillageResourceFields { get; set; }
        #endregion

        #region Building
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingEffect> BuildingsEffects { get; set; }
        public DbSet<BuildingResource> BuildingsResource { get; set; }
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

            modelBuilder.Entity<BuildingResource>()
                .HasKey(pc => new { pc.BuildingId, pc.ResourceId });

            modelBuilder.Entity<BuildingResource>()
                .HasOne(pc => pc.Building)
                .WithMany(p => p.BuildingResources)
                .HasForeignKey(pc => pc.BuildingId);

            modelBuilder.Entity<BuildingResource>()
                .HasOne(pc => pc.Resource)
                .WithMany(c => c.BuildingResources)
                .HasForeignKey(pc => pc.ResourceId);

            modelBuilder.Entity<BuildingEffect>()
                .HasKey(pc => new { pc.BuildingId, pc.EffectId });

            modelBuilder.Entity<BuildingEffect>()
                .HasOne(pc => pc.Building)
                .WithMany(p => p.BuildingEffects)
                .HasForeignKey(pc => pc.BuildingId);

            modelBuilder.Entity<BuildingEffect>()
                .HasOne(pc => pc.Effect)
                .WithMany(c => c.BuildingEffects)
                .HasForeignKey(pc => pc.EffectId);

            modelBuilder.Entity<VillageBuilding>()
                .HasKey(pc => new { pc.BuildingId, pc.VillageId });

            modelBuilder.Entity<VillageBuilding>()
                .HasOne(pc => pc.Building)
                .WithMany(p => p.VillageBuildings)
                .HasForeignKey(pc => pc.BuildingId);

            modelBuilder.Entity<VillageBuilding>()
                .HasOne(pc => pc.Village)
                .WithMany(c => c.VillageBuildings)
                .HasForeignKey(pc => pc.VillageId);

            modelBuilder.Entity<Resource>().HasData(
                new Resource { Id = 1, Name = "Wood", Enabled = true, StartingAmount = 500, EffectId = 2 },
                new Resource { Id = 2, Name = "Bricks", Enabled = true, StartingAmount = 500, EffectId = 2 },
                new Resource { Id = 3, Name = "Iron", Enabled = true, StartingAmount = 500, EffectId = 2 },
                new Resource { Id = 4, Name = "Grain", Enabled = true, StartingAmount = 500, EffectId = 3 },
                new Resource { Id = 5, Name = "Gold", Enabled = true, StartingAmount = 20, EffectId = 4 }
            );

            modelBuilder.Entity<ResourceField>().HasData(
                new ResourceField { Id = 1, Name = "Forest", Description = "Description", Enabled = true, ResourceId = 1, MaxLevel = 50, BaseBuildDuration = 10, BuildCoefficient = 1.7 },
                new ResourceField { Id = 2, Name = "Clay", Description = "Description", Enabled = true, ResourceId = 2, MaxLevel = 50, BaseBuildDuration = 12, BuildCoefficient = 1.5 },
                new ResourceField { Id = 3, Name = "Iron mine", Description = "Description", Enabled = true, ResourceId = 3, MaxLevel = 50, BaseBuildDuration = 20, BuildCoefficient = 1.4 },
                new ResourceField { Id = 4, Name = "Grain field", Description = "Description", Enabled = true, ResourceId = 4, MaxLevel = 50, BaseBuildDuration = 7, BuildCoefficient = 1.3 }
            );

            modelBuilder.Entity<Effect>().HasData(
                new Effect { Id = 1, Name = "Build time", Description = "Description" },
                new Effect { Id = 2, Name = "Storage capacity", Description = "Description" },
                new Effect { Id = 3, Name = "Granary capacity", Description = "Description" },
                new Effect { Id = 4, Name = "Gold capacity", Description = "Description" }
            );

            modelBuilder.Entity<Building>().HasData(
                new Building { Id = 1, Name = "Castle", BaseBuildDuration = 10, BuildCoefficient = 1.5, MaxLevel = 20, Enabled = true, Description = "Description" },
                new Building { Id = 2, Name = "Storage", BaseBuildDuration = 30, BuildCoefficient = 1.3, MaxLevel = 20, Enabled = true, Description = "Description" },
                new Building { Id = 3, Name = "Granary", BaseBuildDuration = 20, BuildCoefficient = 1.2, MaxLevel = 20, Enabled = true, Description = "Description" },
                new Building { Id = 3, Name = "Bank", BaseBuildDuration = 20, BuildCoefficient = 1.2, MaxLevel = 20, Enabled = true, Description = "Description" }
            );

            modelBuilder.Entity<Cost>().HasData(
                new Cost { Id = 1, BaseCost = 50, Coefficient = 1.4 },
                new Cost { Id = 2, BaseCost = 80, Coefficient = 1.6 },
                new Cost { Id = 3, BaseCost = 40, Coefficient = 1.3 },
                new Cost { Id = 4, BaseCost = 20, Coefficient = 1.2 },
                new Cost { Id = 5, BaseCost = 30, Coefficient = 1.2 },
                new Cost { Id = 6, BaseCost = 50, Coefficient = 1.5 },
                new Cost { Id = 7, BaseCost = 30, Coefficient = 1.4 },
                new Cost { Id = 8, BaseCost = 40, Coefficient = 1.1 },
                new Cost { Id = 9, BaseCost = 30, Coefficient = 1.2 },
                new Cost { Id = 10, BaseCost = 50, Coefficient = 1.5 },
                new Cost { Id = 11, BaseCost = 20, Coefficient = 1.4 },
                new Cost { Id = 12, BaseCost = 50, Coefficient = 1.4 },
                new Cost { Id = 13, BaseCost = 1000, Coefficient = 1.2 },
                new Cost { Id = 14, BaseCost = 1200, Coefficient = 1.5 },
                new Cost { Id = 15, BaseCost = 1500, Coefficient = 1.4 },
                new Cost { Id = 16, BaseCost = 300, Coefficient = 1.4 }
            );

            modelBuilder.Entity<BuildingResource>().HasData(
                new BuildingResource { Id = 1, BuildingId = 1, ResourceId = 1, CostId = 1 },
                new BuildingResource { Id = 2, BuildingId = 1, ResourceId = 2, CostId = 2 },
                new BuildingResource { Id = 3, BuildingId = 1, ResourceId = 3, CostId = 3 },
                new BuildingResource { Id = 4, BuildingId = 1, ResourceId = 4, CostId = 4 },
                new BuildingResource { Id = 5, BuildingId = 2, ResourceId = 1, CostId = 5 },
                new BuildingResource { Id = 6, BuildingId = 2, ResourceId = 2, CostId = 6 },
                new BuildingResource { Id = 7, BuildingId = 2, ResourceId = 3, CostId = 7 },
                new BuildingResource { Id = 8, BuildingId = 2, ResourceId = 4, CostId = 8 },
                new BuildingResource { Id = 9, BuildingId = 3, ResourceId = 1, CostId = 9 },
                new BuildingResource { Id = 10, BuildingId = 3, ResourceId = 2, CostId = 10 },
                new BuildingResource { Id = 11, BuildingId = 3, ResourceId = 3, CostId = 11 },
                new BuildingResource { Id = 12, BuildingId = 3, ResourceId = 4, CostId = 12 },
                new BuildingResource { Id = 13, BuildingId = 4, ResourceId = 1, CostId = 13 },
                new BuildingResource { Id = 14, BuildingId = 4, ResourceId = 2, CostId = 14 },
                new BuildingResource { Id = 15, BuildingId = 4, ResourceId = 3, CostId = 15 },
                new BuildingResource { Id = 16, BuildingId = 4, ResourceId = 4, CostId = 16 }
            );

            modelBuilder.Entity<BuildingEffect>().HasData(
                new BuildingEffect { Id = 1, BuildingId = 1, EffectId = 1, StartingValue = 100, Coefficient = -0.03 },
                new BuildingEffect { Id = 2, BuildingId = 2, EffectId = 2, StartingValue = 800, Coefficient = 1.7 },
                new BuildingEffect { Id = 3, BuildingId = 3, EffectId = 3, StartingValue = 800, Coefficient = 1.7 },
                new BuildingEffect { Id = 4, BuildingId = 4, EffectId = 4, StartingValue = 100, Coefficient = 1.4 }
            );
        }
    }
}
