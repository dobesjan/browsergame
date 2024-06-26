using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Buildings
{
    public class BuildingRequirement : Entity
    {
        public int BuildingId { get; set; }

        [ForeignKey(nameof(BuildingId))]
        public Building Building { get; set; }

        public int RequiredForBuildingId { get; set; }

        [ForeignKey(nameof(RequiredForBuildingId))]
        public Building RequiredForBuilding { get; set; }

        public int Level { get; set; }
    }
}
