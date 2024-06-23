using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Buildings
{
    public class BuildingBase : Entity
    {
        [Required]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public string Description { get; set; }

        public int MaxLevel { get; set; }

        public int BaseBuildDuration { get; set; }

        public double BuildCoefficient { get; set; }

        [ValidateNever]
        public List<BuildingResource> BuildingResources { get; set; }

        public int GetBuildDuration(int level)
        {
            // Use an exponential formula to calculate the build duration
            // Cost = BaseBuildDuration * (BuildCoefficient ^ (level - 1))
            return (int)(BaseBuildDuration * Math.Pow(BuildCoefficient, level - 1));
        }
    }
}
