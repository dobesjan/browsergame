using BrowserGame.BusinessLayer.Exceptions;
using BrowserGame.BusinessLayer.Resources;
using BrowserGame.DataAccess.UnitOfWork;
using BrowserGame.Models.Buildings;
using BrowserGame.Models.Villages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Villages
{
    public class BuildingService : IBuildingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVillageService _villageService;
        private readonly IGameResourceService _gameResourceService;
        private readonly ILogger<BuildingService> _logger;

        public BuildingService(IUnitOfWork unitOfWork, IVillageService villageService, IGameResourceService gameResourceService, ILogger<BuildingService> logger)
        {
            _unitOfWork = unitOfWork;
            _villageService = villageService;
            _gameResourceService = gameResourceService;
            _logger = logger;
        }

        public void AddBuildOrder(int villageId, int slotId, int buildingId)
        {
            var village = _villageService.GetVillage(villageId);
            AddBuildOrder(village, slotId, buildingId);
        }

        public void AddBuildOrder(Village village, int slotId, int buildingId)
        {
            // TODO: Refactor it to work with slots as well

            // TODO: Make slot range configurable
            if (slotId < 0 || slotId > 20) throw new GameException($"Wrong slot with id '{slotId}'");

            int level = 0;

            VillageBuilding villageBuilding = null;

            if (village.VillageBuildings != null)
            {
                villageBuilding = village.VillageBuildings.FirstOrDefault(vb => vb.SlotId == slotId);
            }

            if (villageBuilding != null && villageBuilding.Building.Id != buildingId) throw new GameException("Wrong building");
            if (villageBuilding != null && villageBuilding.Building.Id == buildingId)
            {
                level = villageBuilding.Level;
            }

            int nextLevel = level + 1;

            if (!_gameResourceService.HasEnoughResources(village, level, buildingId)) throw new GameException("Not enough resources");
            if (!CheckAndUpdateBuildProcess(village)) throw new GameException("Not enough build queue capacity");

            //TODO: Slots validation??

            // Add to build queue
            if (villageBuilding == null)
            {
                villageBuilding = new VillageBuilding
                {
                    VillageId = village.Id,
                    BuildingId = buildingId,
                    SlotId = slotId,
                    Level = 0
                };

                _unitOfWork.VillageBuildingRepository.Add(villageBuilding);
                _unitOfWork.VillageBuildingRepository.Save();
            }

            var duration = villageBuilding.Building.GetBuildDuration(nextLevel);
            AddBuildQueueItem(village, villageBuilding.Id, duration, nextLevel);
        }

        public void AddResourceFieldBuildOrder(int villageId, int resourceFieldId)
        {
            var village = _villageService.GetVillage(villageId);
            AddResourceFieldBuildOrder(village, resourceFieldId);
        }

        public void AddResourceFieldBuildOrder(Village village, int resourceFieldId)
        {
            if (village.VillageFields == null) throw new GameException($"Village fields not found for village with id '{village.Id}'");

            var villageField = village.VillageFields.FirstOrDefault(r => r.Id == resourceFieldId);
            if (villageField == null) throw new GameException($"Resource field with id '{resourceFieldId}' not found for village with id '{village.Id}'");

            int nextLevel = villageField.Level + 1;

            if (!_gameResourceService.HasEnoughResources(village, villageField.Level, villageField.ResourceField.Id)) throw new GameException("Not enough resources");
            if (!CheckAndUpdateBuildProcess(village)) throw new GameException("Not enough build queue capacity");

            var duration = villageField.ResourceField.GetBuildDuration(nextLevel);
            AddResourceFieldToBuildQueue(village, villageField.Id, duration, nextLevel);
        }

        public bool CheckAndUpdateBuildProcess(int villageId)
        {
            var village = _villageService.GetVillage(villageId);
            return CheckAndUpdateBuildProcess(village);
        }

        public bool CheckAndUpdateBuildProcess(Village village)
        {
            if (village.BuildQueueItems == null)
            {
                return true;
            }

            var buildQueue = village.BuildQueueItems.OrderBy(b => b.BuildOrder).ToList();

            // Check if some of them are completed
            foreach (var item in buildQueue)
            {
                if (item.IsBuildFinished())
                {
                    // Increase level of building
                    if (item.VillageBuilding != null && item.VillageBuildingId.HasValue)
                    {
                        item.VillageBuilding.Level = item.TargetLevel;

                        _unitOfWork.VillageBuildingRepository.Update(item.VillageBuilding);
                        _unitOfWork.VillageBuildingRepository.Save();
                    }
                    else if (item.VillageResourceField != null && item.VillageResourceFieldId.HasValue)
                    {
                        item.VillageResourceField.Level = item.TargetLevel;

                        _unitOfWork.VillageResourceFieldRepository.Update(item.VillageResourceField);
                        _unitOfWork.VillageResourceFieldRepository.Save();
                    }

                    // Remove from queue
                    _unitOfWork.BuildQueueItemRepository.Remove(item);
                    _unitOfWork.BuildQueueItemRepository.Save();

                    if (buildQueue.Any(b => b.Id == item.Id))
                    {
                        buildQueue.Remove(item);
                    }
                }
            }

            // TODO: Make max queue length configurable
            if (buildQueue != null && buildQueue.Count > 2)
            {
                return false;
            }

            return true;
        }

        private int CalculateBuildOrder(Village village)
        {
            int buildOrder = 1;

            if (village.BuildQueueItems != null && village.BuildQueueItems.Any())
            {
                buildOrder = village.BuildQueueItems.OrderBy(q => q.BuildOrder).Last().BuildOrder + 1;
            }

            return buildOrder;
        }

        private void AddBuildQueueItem(Village village, int villageBuildingId, int buildDuration, int nextLevel)
        {
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddSeconds(buildDuration);

            int buildOrder = CalculateBuildOrder(village);

            var item = new BuildQueueItem
            {
                VillageId = village.Id,
                VillageBuildingId = villageBuildingId,
                BuildStart = startTime,
                BuildEnd = endTime,
                BuildOrder = buildOrder,
                TargetLevel = nextLevel
            };

            _unitOfWork.BuildQueueItemRepository.Add(item);
            _unitOfWork.BuildQueueItemRepository.Save();
        }

        private void AddResourceFieldToBuildQueue(Village village, int villageResourceFieldId, int buildDuration, int nextLevel)
        {
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddSeconds(buildDuration);

            int buildOrder = CalculateBuildOrder(village);

            var item = new BuildQueueItem
            {
                VillageId = village.Id,
                VillageResourceFieldId = villageResourceFieldId,
                BuildStart = startTime,
                BuildEnd = endTime,
                BuildOrder = buildOrder,
                TargetLevel = nextLevel
            };

            _unitOfWork.BuildQueueItemRepository.Add(item);
            _unitOfWork.BuildQueueItemRepository.Save();
        }
    }
}
