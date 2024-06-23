using BrowserGame.BusinessLayer.Villages;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc;

namespace BrowserGame.Controllers
{
    public class ResourceController : BrowserBaseController
    {
        public ResourceController(ILogger<ResourceController> logger, IUnitOfWork unitOfWork, IVillageService villageService) : base(logger, unitOfWork, villageService)
        {
            
        }

        public IActionResult Index(int villageId)
        {
            return GetVillageView(villageId);
        }
    }
}
