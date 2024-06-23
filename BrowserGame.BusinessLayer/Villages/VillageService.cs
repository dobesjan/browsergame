using BrowserGame.BusinessLayer.Resources;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models.Villages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Villages
{
    public class VillageService : IVillageService
    {
        private readonly ILogger<VillageService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameResourceService _resourceService;

        public VillageService(ILogger<VillageService> logger, IUnitOfWork unitOfWork, IGameResourceService resourceService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _resourceService = resourceService;
        }

        private Village GetVillageInternal(Village village)
        {
            if (village == null)
            {
                var message = $"Village not found";
                _logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            return village;
        }

        public Village GetVillage(int villageId)
        {
            var village = _unitOfWork.VillageRepository.GetVillage(villageId);
            return GetVillageInternal(village);
        }

        public Village GetVillage(int villageId, int playerId)
        {
            var village = _unitOfWork.VillageRepository.GetVillage(villageId, playerId);
            return GetVillageInternal(village);
        }

        public void CreateVillage(int playerId)
        {
            var player = _unitOfWork.PlayerRepository.Get(playerId);
            if (player != null) 
            {
                var message = $"Player with id '{playerId}' not found.";
                _logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            // Initialize location - maybe later when map will be implemented

            // Initialize start resources
            var village = new Village
            {
                Name = player.Name,
                PlayerId = playerId
            };

            _unitOfWork.VillageRepository.Add(village);
            _unitOfWork.VillageRepository.Save();

            _resourceService.InitVillageResources(village.Id);
            _resourceService.InitResourceFields(village.Id);
        }

        public double GetEffectValue(int villageId, int effectId)
        {
            var village = GetVillage(villageId);

            double effectValue = 0;

            if (village.VillageBuildings != null)
            {
                foreach (var villageBuilding in village.VillageBuildings)
                {
                    if (villageBuilding.Building.BuildingEffects != null)
                    {
                        var effect = villageBuilding.Building.BuildingEffects.FirstOrDefault(e => e.Id == effectId);
                        if (effect != null)
                        {
                            effectValue += effect.GetValue(villageBuilding.Level);
                        }
                    }
                    
                }
            }

            return effectValue;
        }
    }
}
