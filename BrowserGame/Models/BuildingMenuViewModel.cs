using BrowserGame.Models.Buildings;
using BrowserGame.Models.Villages;

namespace BrowserGame.Models
{
    public class BuildingMenuViewModel
    {
        public Village Village { get; set; }
        public List<Building> AvailableBuildings { get; set; }
    }
}
