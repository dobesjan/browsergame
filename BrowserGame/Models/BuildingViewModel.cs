using BrowserGame.Models.Buildings;
using BrowserGame.Models.Villages;

namespace BrowserGame.Models
{
    public class BuildingViewModel
    {
        public Village Village { get; set; }
        public int SlotId { get; set; }
        public Building Building { get; set; }
    }
}
