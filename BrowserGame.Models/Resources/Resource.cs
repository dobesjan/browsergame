using BrowserGame.Models.Buildings;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Resources
{
    public class Resource : Entity
    {
        [Required]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        // Amount how much of this resource will player obtain in the game start
        public int StartingAmount { get; set; }

        [ValidateNever]
        public List<VillageResource> VillageResources { get; set; }

        [ValidateNever]
        public List<BuildingResource> BuildingResources { get; set; }
    }
}
