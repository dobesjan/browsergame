using BrowserGame.DataAccess.Repository.Users;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrowserGame.Controllers
{
    public class BrowserBaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly IUnitOfWork _unitOfWork;

        public BrowserBaseController(ILogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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
    }
}
