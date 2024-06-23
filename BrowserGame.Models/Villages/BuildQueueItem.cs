using BrowserGame.Models.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Villages
{
    public class BuildQueueItem : Entity
    {
        public int VillageId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(VillageId))]
        public Village Village { get; set; }

        [ValidateNever]
        public int? VillageBuildingId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(VillageBuildingId))]
        public VillageBuilding? VillageBuilding { get; set; }

        [ValidateNever]
        public int? VillageResourceFieldId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(VillageResourceFieldId))]
        public VillageResourceField? VillageResourceField { get; set; }

        public DateTime BuildStart { get; set; }

        public DateTime BuildEnd { get; set; }

        public int BuildOrder { get; set; }

        public int TargetLevel { get; set; }

        public bool IsBuildFinished()
        {
            return DateTime.UtcNow >= BuildEnd;
        }
    }
}
