using BrowserGame.BusinessLayer.Villages;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models;
using BrowserGame.Models.Buildings;
using BrowserGame.Models.Resources;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc;

namespace BrowserGame.Controllers
{
    public class BuildController : BrowserBaseController
    {
        private readonly IBuildingService _buildingService;

        [BindProperty]
        public BuildingViewModel BuildingViewModel { get; set; }

        [BindProperty]
        public BuildingMenuViewModel BuildingMenuViewModel { get; set; }

        public BuildController(ILogger<AccountController> logger, IUnitOfWork unitOfWork, IVillageService villageService, IBuildingService buildingService) : base(logger, unitOfWork, villageService)
        {
            _buildingService = buildingService;
        }

        public IActionResult Index(int villageId, int slotId, int buildingId)
        {
            Village village = GetVillage(villageId);
            Building building = _unitOfWork.BuildingRepository.Get(buildingId);

            BuildingViewModel = new BuildingViewModel
            {
                Village = village,
                SlotId = slotId,
                Building = building
            };

            return View(BuildingViewModel);
        }

        public IActionResult BuildMenu(int villageId, int slotId)
        {
            if (!_buildingService.ValidateBuildingSlots(slotId)) return Redirect("/");

            Village village = GetVillage(villageId);
            BuildingMenuViewModel = new BuildingMenuViewModel
            {
                Village = village,
                AvailableBuildings = _buildingService.GetAvailableBuildings(village)
            };

            return View(BuildingMenuViewModel);
        }

        [HttpGet]
        public IActionResult Build(int villageId, int slotId, int buildingId)
        {
            return HandleResponse(() =>
            {
                _buildingService.AddBuildOrder(villageId, slotId, buildingId);
                return Redirect("/");
            }, Redirect("/"));
        }

        [HttpGet]
        public IActionResult LevelUpResourceField(int villageId, int resourceFieldId)
        {
            return HandleResponse(() =>
            {
                _buildingService.AddResourceFieldBuildOrder(villageId, resourceFieldId);
                return Redirect("/");
            }, Redirect("/"));
        }
    }
}
