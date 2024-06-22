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

        public VillageService(ILogger<VillageService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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
                Name = player.Name
            };

            _unitOfWork.VillageRepository.Add(village);
            _unitOfWork.VillageRepository.Save();

            var resources = _unitOfWork.ResourceRepository.GetResources(true);

            foreach (var resource in resources)
            {
                var villageResource = new VillageResource
                {
                    VillageId = village.Id,
                    ResourceId = resource.Id,
                    RealAmount = resource.StartingAmount,
                    LastAmountCalculation = DateTime.UtcNow,
                };

                _unitOfWork.VillageResourceRepository.Add(villageResource);
            }

            _unitOfWork.VillageResourceRepository.Save();

            // Intialize fields
            var resourceFields = _unitOfWork.ResourceFieldRepository.GetResourceFields(true);

            foreach (var field in resourceFields)
            {
                //TODO: Resolve production per hour
                //TODO: Consider village resource matrix
                var villageResourceField = new VillageResourceField
                {
                    VillageId = village.Id,
                    ResourceFieldId = field.Id,
                    ProductionPerHour = 10
                };

                _unitOfWork.VillageResourceFieldRepository.Add(villageResourceField);
            }

            _unitOfWork.VillageResourceFieldRepository.Save();

            // Initialize buildings
        }

        public double GetEffectValue(int villageId, int effectId)
        {
            var village = _unitOfWork.VillageRepository.GetVillage(villageId);

            if (village == null)
            {
                var message = $"Village not found";
                _logger.LogError(message);
                throw new ArgumentNullException(message);
            }

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
