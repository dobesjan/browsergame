using BrowserGame.Models.Villages;

namespace BrowserGame.Models
{
    public class VillageViewModel
    {
        public Village Village { get; set; }
        public List<BuildingSpaceViewModel> BuildingSpaces { get; set; }
    }
}
