using BrowserGame.Models.Resources;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Buildings
{
    public class BuildingResource : Entity
    {
        public int BuildingId { get; set; }

        [ValidateNever]
        public Building Building { get; set; }

        public int ResourceId { get; set; }

        [ValidateNever]
        public Resource Resource { get; set; }

        public int CostId { get; set; }

        [ForeignKey(nameof(CostId))]
        public Cost Cost { get; set; }
    }
}
