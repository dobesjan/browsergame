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
        public ResourceBuildingViewModel ResourceBuildingViewModel { get; set; }

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

        [HttpPost]
        public IActionResult Index(BuildingViewModel vm)
        {
            return HandleResponse(() =>
            {
                _buildingService.AddBuildOrder(vm.Village.Id, vm.SlotId, vm.Building.Id);
                return Redirect("/");
            }, Redirect("/"));
        }

        public IActionResult BuildMenu(int villageId, int slotId)
        {
            if (!_buildingService.ValidateBuildingSlots(slotId)) return Redirect("/");
            // TODO: Consider how to resolve requirements

            return View();
        }

        public IActionResult LevelUpResourceField(int villageId, int resourceFieldId) 
        {
            Village village = GetVillage(villageId);

            ResourceBuildingViewModel = new ResourceBuildingViewModel
            {
                Village = village,
                VillageResourceFieldId = resourceFieldId
            };

            return View(ResourceBuildingViewModel);
        }

        [HttpPost]
        public IActionResult LevelUpResourceField(ResourceBuildingViewModel vm)
        {
            return HandleResponse(() =>
            {
                _buildingService.AddResourceFieldBuildOrder(vm.Village.Id, vm.VillageResourceFieldId);
                return Redirect("/");
            }, Redirect("/"));
        }
    }
}
