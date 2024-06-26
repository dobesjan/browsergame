using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BrowserGame.Models
{
    public class BuildingSpaceViewModel
    {
        public int SlotId { get; set; }
        [ValidateNever]
        public VillageBuilding? VillageBuilding { get; set; }
    }
}
