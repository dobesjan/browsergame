using BrowserGame.BusinessLayer.Villages;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models.Villages;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Resources
{
    public class GameResourceService : IGameResourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVillageService _villageService;
        private readonly ILogger<GameResourceService> _logger;

        public GameResourceService(IUnitOfWork unitOfWork, IVillageService villageService, ILogger<GameResourceService> logger) 
        {
            _unitOfWork = unitOfWork;
            _villageService = villageService;
            _logger = logger;
        }

        public void InitVillageResources(int villageId)
        {
            var resources = _unitOfWork.ResourceRepository.GetResources(true);

            foreach (var resource in resources)
            {
                var villageResource = new VillageResource
                {
                    VillageId = villageId,
                    ResourceId = resource.Id,
                    RealAmount = resource.StartingAmount,
                    LastAmountCalculation = DateTime.UtcNow,
                };

                _unitOfWork.VillageResourceRepository.Add(villageResource);
            }

            _unitOfWork.VillageResourceRepository.Save();
        }

        public void InitResourceFields(int villageId)
        {
            var resourceFields = _unitOfWork.ResourceFieldRepository.GetResourceFields(true);

            foreach (var field in resourceFields)
            {
                //TODO: Resolve production per hour
                //TODO: Consider village resource matrix
                var villageResourceField = new VillageResourceField
                {
                    VillageId = villageId,
                    ResourceFieldId = field.Id,
                    ProductionPerHour = 10
                };

                _unitOfWork.VillageResourceFieldRepository.Add(villageResourceField);
            }

            _unitOfWork.VillageResourceFieldRepository.Save();
        }

        //TODO: Consider production calculation flow
        // What to do when building is ranked up?
        public void CalculateProductionForVillage(int villageId)
        {
            var village = _unitOfWork.VillageRepository.Get(villageId);

            if (village == null)
            {
                var message = $"Village with id '{villageId}' not found";
                _logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            if (!village.VillageResources.Any())
            {
                var message = $"Village resources not found for village with id '{villageId}'";
                _logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            if (!village.VillageFields.Any()) 
            {
                var message = $"Village fields not found for village with id '{villageId}'";
                _logger.LogError(message);
                throw new ArgumentNullException(message);
            }

            foreach (var res in village.VillageResources)
            {
                var diff = res.AmountCalculationDifference;

                //TODO: Consider move to separate method
                // Calculation of production per hour
                var resourceFields = village.VillageFields.Where(f => f.ResourceFieldId == res.ResourceId).ToList();
                double productionPerSecond = 0;
                productionPerSecond = resourceFields.Sum(rf => rf.RealProductionPerSecond);

                // TODO: Make this variable configurable
                var resourceCapacity = 1000 + _villageService.GetEffectValue(villageId, res.Resource.EffectId);

                // Calculation of real production
                //TODO: COnsider move as well
                int seconds = diff.Seconds;
                double realProduction = productionPerSecond * seconds;
                res.AddAmount(realProduction, resourceCapacity);
            }

            _unitOfWork.VillageRepository.Update(village);
            _unitOfWork.VillageRepository.Save();
        }

        public bool HasEnoughResources(int villageId, int level, int buildingId)
        {
            var village = _villageService.GetVillage(villageId);
            return HasEnoughResources(village, level, buildingId);
        }

        public bool HasEnoughResources(Village village, int level, int buildingId)
        {
            var building = _unitOfWork.BuildingBaseRepository.Get(buildingId);
            if (building == null) throw new InvalidDataException("Building not found");

            foreach (var resource in village.VillageResources)
            {
                foreach (var buildingResource in building.BuildingResources)
                {
                    if (resource.Amount < buildingResource.Cost.GetCost(level)) return false;
                }

            }

            return false;
        }
    }
}
