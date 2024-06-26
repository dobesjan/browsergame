using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Buildings
{
    public class Building : BuildingBase
    {
        [ValidateNever]
        public List<BuildingEffect> BuildingEffects { get; set; }

        [ValidateNever]
        public List<VillageBuilding> VillageBuildings { get; set; }

        [InverseProperty(nameof(Building))]
        [ValidateNever]
        public List<BuildingRequirement> BuildingRequirements { get; set; }
    }
}
