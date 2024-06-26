using BrowserGame.BusinessLayer.Villages;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc;

namespace BrowserGame.Controllers
{
    public class VillageController : BrowserBaseController
    {
        [BindProperty]
        public VillageViewModel VillageViewModel { get; set; }

        public VillageController(ILogger<VillageController> logger, IUnitOfWork unitOfWork, IVillageService villageService) : base(logger, unitOfWork, villageService)
        {
        }

        public IActionResult Index(int villageId)
        {
            Village village = GetVillage(villageId);

            VillageViewModel = new VillageViewModel
            {
                Village = village,
                BuildingSpaces = new List<BuildingSpaceViewModel>()
            };

            //TODO: Make it configurable
            for (int i=0; i <= 20; i++)
            {
                BuildingSpaceViewModel buildingSpace = new BuildingSpaceViewModel
                {
                    SlotId = i
                };

                if (village.VillageBuildings != null && village.VillageBuildings.Any())
                {
                    var villageBuilding = village.VillageBuildings.FirstOrDefault(b => b.SlotId == i);
                    if (villageBuilding != null)
                    {
                        buildingSpace.VillageBuilding = villageBuilding;
                    }
                }

                VillageViewModel.BuildingSpaces.Add(buildingSpace);
            }

            return View(VillageViewModel);
        }
    }
}
