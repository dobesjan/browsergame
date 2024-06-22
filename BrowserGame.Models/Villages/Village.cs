using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Villages
{
    public class Village : Entity
    {
        [Required]
        public string Name { get; set; }

        [ValidateNever]
        public List<VillageResource>? VillageResources { get; set; }

        [ValidateNever]
        public List<VillageResourceField>? VillageFields { get; set; }

        [ValidateNever]
        public List<VillageBuilding>? VillageBuildings { get; set; }

        [ValidateNever]
        [InverseProperty(nameof(Village))]
        public List<BuildQueueItem>? BuildQueueItems { get; set; }
    }
}
