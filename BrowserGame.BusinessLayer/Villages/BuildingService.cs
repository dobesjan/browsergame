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
            // TODO: Make slot range configurable
            if (slotId < 0 || slotId > 20) throw new InvalidDataException($"Wrong slot with id '{slotId}'");

            int level = 0;

            VillageBuilding villageBuilding = null;

            if (village.VillageBuildings != null)
            {
                villageBuilding = village.VillageBuildings.FirstOrDefault(vb => vb.SlotId == slotId);
            }

            if (villageBuilding != null && villageBuilding.Building.Id != buildingId) throw new InvalidDataException("Wrong building");
            if (villageBuilding != null && villageBuilding.Building.Id == buildingId)
            {
                level = villageBuilding.Level;
            }

            int nextLevel = level + 1;

            if (!_gameResourceService.HasEnoughResources(village, level, buildingId)) throw new InvalidDataException("Not enough resources");
            if (!CheckAndUpdateBuildProcess(village)) throw new InvalidDataException("Not enough build queue capacity");

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

            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddSeconds(villageBuilding.Building.GetBuildDuration(nextLevel));

            int buildOrder = 1;

            if (village.BuildQueueItems != null && village.BuildQueueItems.Any())
            {
                buildOrder = village.BuildQueueItems.Last().BuildOrder + 1;
            }

            var item = new BuildQueueItem
            {
                VillageId = village.Id,
                VillageBuildingId = buildingId,
                BuildStart = startTime,
                BuildEnd = endTime,
                BuildOrder = buildOrder,
                TargetLevel = nextLevel
            };

            _unitOfWork.BuildQueueItemRepository.Add(item);
            _unitOfWork.BuildQueueItemRepository.Save();
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
    }
}
