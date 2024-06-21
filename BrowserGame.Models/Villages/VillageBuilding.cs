using BrowserGame.Models.Buildings;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Villages
{
    public class VillageBuilding : Entity
    {
        public int VillageId { get; set; }

        [ValidateNever]
        public Village Village { get; set; }

        public int BuildingId { get; set; }

        [ValidateNever]
        public Building Building { get; set; }
    }
}
