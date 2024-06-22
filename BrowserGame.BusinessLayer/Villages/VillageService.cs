using BrowserGame.DataAccess.UnitOfWork;
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
            // Get player and check if exists

            // Initialize location - maybe later when map will be implemented

            // Initialize start resources

            // Intialize fields

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
