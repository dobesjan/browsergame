using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models.Villages;
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
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResourceService> _logger;

        public ResourceService(IUnitOfWork unitOfWork, ILogger<ResourceService> logger) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

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

                //TODO: Move to separate method
                // Calculation of production per hour
                var resourceFields = village.VillageFields.Where(f => f.ResourceFieldId == res.ResourceId).ToList();
                double productionPerSecond = 0;
                foreach (var field in resourceFields)
                {
                    productionPerSecond += field.RealProductionPerSecond;
                }

                // Calculation of real production
                //TODO: COnsider move as well
                int seconds = diff.Seconds;
                double realProduction = productionPerSecond * seconds;
                res.AddAmount(realProduction);
            }

            _unitOfWork.VillageRepository.Update(village);
            _unitOfWork.VillageRepository.Save();
        }
    }
}
