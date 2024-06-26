using BrowserGame.BusinessLayer.Exceptions;
using BrowserGame.BusinessLayer.Villages;
using BrowserGame.DataAccess.Repository.Users;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models.Users;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrowserGame.Controllers
{
    public class BrowserBaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IVillageService _villageService;

        public BrowserBaseController(ILogger logger, IUnitOfWork unitOfWork, IVillageService villageService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _villageService = villageService;
        }

        public Player GetPlayer()
        {
            Player player = null;

            if (User != null && User.Identity != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        var message = $"UserId: {userId} not found for this user";
                        _logger.LogWarning(message);
                        throw new UnauthorizedAccessException(message);
                    }

                    player = _unitOfWork.PlayerRepository.GetByUserId(userId);
                    if (player == null)
                    {
                        var message = $"Authenticated player with identifier: {userId} not found in database even when user is authenticated";
                        _logger.LogWarning(message);
                        throw new UnauthorizedAccessException(message);
                    }
                }
            }

            return player;
        }

        protected Village GetVillage(int villageId)
        {
            var player = GetPlayer();
            return _villageService.GetVillage(villageId, player.Id);
        }

        protected IActionResult GetVillageView(int villageId) 
        {
            Village village = GetVillage(villageId);
            return View(village);
        }

        protected IActionResult HandleResponse(Func<IActionResult> action, IActionResult errorResult)
        {
            try
            {
                return action();
            }
            catch (GameException ex)
            {
                _logger.LogInformation(ex, ex.Message);
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return errorResult;
        }
    }
}
