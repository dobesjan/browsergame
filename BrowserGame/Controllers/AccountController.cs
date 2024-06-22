using Auth0.AspNetCore.Authentication;
using BrowserGame.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using BrowserGame.Models.Users;
using BrowserGame.BusinessLayer.Villages;

namespace BrowserGame.Controllers
{
    public class AccountController : BrowserBaseController
    {
        private readonly IVillageService _villageService;

        public AccountController(ILogger<AccountController> logger, IUnitOfWork unitOfWork, IVillageService villageService) : base(logger, unitOfWork) 
        {
            _villageService = villageService;
        }

        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a login.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Callback URLs** settings for the app.
                .WithRedirectUri(Url.Action("Callback", "Account", new { ReturnUrl = returnUrl }, Request.Scheme))
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be added to the
                // **Allowed Logout URLs** settings for the app.
                .WithRedirectUri("/")
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(new
            {
                Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }

        [Authorize]
        public IActionResult Callback(string returnUrl = "/")
        {
            if (User != null && !User.Claims.IsNullOrEmpty() && User.Identity != null)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var player = _unitOfWork.PlayerRepository.GetByUserId(userId);
                if (player == null)
                {
                    player = new Player
                    {
                        UserId = userId,
                        Name = User.Identity?.Name,
                    };

                    _unitOfWork.PlayerRepository.Add(player);
                    _unitOfWork.PlayerRepository.Save();

                    // Initialize starting resources in game
                    player = _unitOfWork.PlayerRepository.Get(p => p.UserId == userId);
                    _villageService.CreateVillage(player.Id);
                }
            }

            return LocalRedirect(returnUrl);
        }
    }
}
