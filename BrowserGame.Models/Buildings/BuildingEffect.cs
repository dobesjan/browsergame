using BrowserGame.Models.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Buildings
{
    public class BuildingEffect : Entity
    {
        public int BuildingId { get; set; }

        [ValidateNever]
        public Building Building { get; set; }

        public int EffectId { get; set; }

        [ValidateNever]
        public Effect Effect { get; set; }

        public double StartingValue { get; set; }

        public double Coefficient { get; set; }

        public double GetValue(int level)
        {
            // Use an exponential formula to calculate the cost
            // Cost = BaseCost * (Coefficient ^ (level - 1))
            return (StartingValue * Math.Pow(Coefficient, level - 1));
        }
    }
}
