using BrowserGame.BusinessLayer.Villages;
using BrowserGame.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BrowserGame.Controllers
{
    public class BuildController : BrowserBaseController
    {
        public BuildController(ILogger<AccountController> logger, IUnitOfWork unitOfWork, IVillageService villageService) : base(logger, unitOfWork, villageService)
        {

        }

        public IActionResult Index(int villageId, int buildingId)
        {
            return View();
        }
    }
}
