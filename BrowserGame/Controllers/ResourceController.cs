using BrowserGame.BusinessLayer.Villages;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc;

namespace BrowserGame.Controllers
{
    public class ResourceController : BrowserBaseController
    {
        [BindProperty]
        public ResourceBuildingViewModel ResourceBuildingViewModel { get; set; }

        public ResourceController(ILogger<ResourceController> logger, IUnitOfWork unitOfWork, IVillageService villageService) : base(logger, unitOfWork, villageService)
        {
            
        }

        public IActionResult Index(int villageId)
        {
            return GetVillageView(villageId);
        }

        public IActionResult Detail(int villageId, int resourceFieldId)
        {
            Village village = GetVillage(villageId);

            ResourceBuildingViewModel = new ResourceBuildingViewModel
            {
                Village = village,
                VillageResourceField = village.VillageFields.FirstOrDefault(v => v.Id == resourceFieldId)
            };

            return View(ResourceBuildingViewModel);
        }
    }
}
