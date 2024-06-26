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
        public DbSet<ResourceFieldResource> ResourceFieldsResource { get; set; }
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
        public DbSet<BuildingRequirement> BuildingsRequirements { get; set; }
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
                .HasKey(pc => new { pc.BuildingBaseId, pc.ResourceId });

            modelBuilder.Entity<BuildingResource>()
                .HasOne(pc => pc.Building)
                .WithMany(p => p.BuildingResources)
                .HasForeignKey(pc => pc.BuildingBaseId);

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
                new ResourceField { Id = 5, Name = "Forest", Description = "Description", Enabled = true, ResourceId = 1, MaxLevel = 50, BaseBuildDuration = 10, BuildCoefficient = 1.7 },
                new ResourceField { Id = 6, Name = "Clay", Description = "Description", Enabled = true, ResourceId = 2, MaxLevel = 50, BaseBuildDuration = 12, BuildCoefficient = 1.5 },
                new ResourceField { Id = 7, Name = "Iron mine", Description = "Description", Enabled = true, ResourceId = 3, MaxLevel = 50, BaseBuildDuration = 20, BuildCoefficient = 1.4 },
                new ResourceField { Id = 8, Name = "Grain field", Description = "Description", Enabled = true, ResourceId = 4, MaxLevel = 50, BaseBuildDuration = 7, BuildCoefficient = 1.3 }
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
                new Building { Id = 4, Name = "Bank", BaseBuildDuration = 20, BuildCoefficient = 1.2, MaxLevel = 20, Enabled = true, Description = "Description" }
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
                new Cost { Id = 16, BaseCost = 300, Coefficient = 1.4 },
                new Cost { Id = 17, BaseCost = 10, Coefficient = 1.2 },
                new Cost { Id = 18, BaseCost = 30, Coefficient = 1.5 },
                new Cost { Id = 19, BaseCost = 50, Coefficient = 1.4 },
                new Cost { Id = 20, BaseCost = 20, Coefficient = 1.6 },
                new Cost { Id = 21, BaseCost = 25, Coefficient = 1.2 },
                new Cost { Id = 22, BaseCost = 30, Coefficient = 1.8 },
                new Cost { Id = 23, BaseCost = 80, Coefficient = 1.1 },
                new Cost { Id = 24, BaseCost = 90, Coefficient = 1.4 },
                new Cost { Id = 25, BaseCost = 50, Coefficient = 1.6 },
                new Cost { Id = 26, BaseCost = 60, Coefficient = 1.5 },
                new Cost { Id = 27, BaseCost = 90, Coefficient = 1.2 },
                new Cost { Id = 28, BaseCost = 30, Coefficient = 1.3 },
                new Cost { Id = 29, BaseCost = 35, Coefficient = 1.7 },
                new Cost { Id = 30, BaseCost = 38, Coefficient = 1.2 },
                new Cost { Id = 31, BaseCost = 35, Coefficient = 1.3 },
                new Cost { Id = 32, BaseCost = 45, Coefficient = 1.4 }
            );

            modelBuilder.Entity<BuildingResource>().HasData(
                new BuildingResource { Id = 1, BuildingBaseId = 1, ResourceId = 1, CostId = 1 },
                new BuildingResource { Id = 2, BuildingBaseId = 1, ResourceId = 2, CostId = 2 },
                new BuildingResource { Id = 3, BuildingBaseId = 1, ResourceId = 3, CostId = 3 },
                new BuildingResource { Id = 4, BuildingBaseId = 1, ResourceId = 4, CostId = 4 },
                new BuildingResource { Id = 5, BuildingBaseId = 2, ResourceId = 1, CostId = 5 },
                new BuildingResource { Id = 6, BuildingBaseId = 2, ResourceId = 2, CostId = 6 },
                new BuildingResource { Id = 7, BuildingBaseId = 2, ResourceId = 3, CostId = 7 },
                new BuildingResource { Id = 8, BuildingBaseId = 2, ResourceId = 4, CostId = 8 },
                new BuildingResource { Id = 9, BuildingBaseId = 3, ResourceId = 1, CostId = 9 },
                new BuildingResource { Id = 10, BuildingBaseId = 3, ResourceId = 2, CostId = 10 },
                new BuildingResource { Id = 11, BuildingBaseId = 3, ResourceId = 3, CostId = 11 },
                new BuildingResource { Id = 12, BuildingBaseId = 3, ResourceId = 4, CostId = 12 },
                new BuildingResource { Id = 13, BuildingBaseId = 4, ResourceId = 1, CostId = 13 },
                new BuildingResource { Id = 14, BuildingBaseId = 4, ResourceId = 2, CostId = 14 },
                new BuildingResource { Id = 15, BuildingBaseId = 4, ResourceId = 3, CostId = 15 },
                new BuildingResource { Id = 16, BuildingBaseId = 4, ResourceId = 4, CostId = 16 },
                new BuildingResource { Id = 17, BuildingBaseId = 5, ResourceId = 1, CostId = 17 },
                new BuildingResource { Id = 18, BuildingBaseId = 5, ResourceId = 2, CostId = 18 },
                new BuildingResource { Id = 19, BuildingBaseId = 5, ResourceId = 3, CostId = 19 },
                new BuildingResource { Id = 20, BuildingBaseId = 5, ResourceId = 4, CostId = 20 },
                new BuildingResource { Id = 21, BuildingBaseId = 6, ResourceId = 1, CostId = 21 },
                new BuildingResource { Id = 22, BuildingBaseId = 6, ResourceId = 2, CostId = 22 },
                new BuildingResource { Id = 23, BuildingBaseId = 6, ResourceId = 3, CostId = 23 },
                new BuildingResource { Id = 24, BuildingBaseId = 6, ResourceId = 4, CostId = 24 },
                new BuildingResource { Id = 25, BuildingBaseId = 7, ResourceId = 1, CostId = 25 },
                new BuildingResource { Id = 26, BuildingBaseId = 7, ResourceId = 2, CostId = 26 },
                new BuildingResource { Id = 27, BuildingBaseId = 7, ResourceId = 3, CostId = 27 },
                new BuildingResource { Id = 28, BuildingBaseId = 7, ResourceId = 4, CostId = 28 },
                new BuildingResource { Id = 29, BuildingBaseId = 8, ResourceId = 1, CostId = 29 },
                new BuildingResource { Id = 30, BuildingBaseId = 8, ResourceId = 2, CostId = 30 },
                new BuildingResource { Id = 31, BuildingBaseId = 8, ResourceId = 3, CostId = 31 },
                new BuildingResource { Id = 32, BuildingBaseId = 8, ResourceId = 4, CostId = 32 }
            );

            modelBuilder.Entity<BuildingEffect>().HasData(
                new BuildingEffect { Id = 1, BuildingId = 1, EffectId = 1, StartingValue = 100, Coefficient = -0.03 },
                new BuildingEffect { Id = 2, BuildingId = 2, EffectId = 2, StartingValue = 800, Coefficient = 1.7 },
                new BuildingEffect { Id = 3, BuildingId = 3, EffectId = 3, StartingValue = 800, Coefficient = 1.7 },
                new BuildingEffect { Id = 4, BuildingId = 4, EffectId = 4, StartingValue = 100, Coefficient = 1.4 }
            );

            modelBuilder.Entity<BuildingRequirement>().HasData(
                new BuildingRequirement { Id = 1, RequiredForBuildingId = 4, BuildingId = 1, Level = 5 }
            );
        }
    }
}
