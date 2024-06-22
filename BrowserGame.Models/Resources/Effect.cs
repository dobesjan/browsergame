using BrowserGame.Models.Buildings;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Resources
{
    //TODO: Consider if needed
    public enum EffectType
    {
        BuildTime = 1,
        StorageCapacity = 2,
        GranaryCapacity = 3
    }

    public class Effect : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [ValidateNever]
        public List<BuildingEffect> BuildingEffects { get; set; }
    }
}
