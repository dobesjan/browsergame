using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Buildings
{
    public class Building : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [ValidateNever]
        public List<BuildingResource> BuildingResources { get; set; }

        [ValidateNever]
        public List<BuildingEffect> BuildingEffects { get; set; }

        [ValidateNever]
        public List<VillageBuilding> VillageBuildings { get; set; }
    }
}
