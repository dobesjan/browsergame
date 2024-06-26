using BrowserGame.Models.Buildings;
using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Villages
{
    public interface IBuildingService
    {
        void AddBuildOrder(int villageId, int slotId, int buildingId);
        void AddBuildOrder(Village village, int slotId, int buildingId);
        List<VillageBuilding> GetVillageBuildings(int villageId);
        List<VillageBuilding> GetVillageBuildings(Village village);
        bool ValidateBuildingSlots(int slotId);
        void AddResourceFieldBuildOrder(int villageId, int resourceFieldId);
        void AddResourceFieldBuildOrder(Village village, int resourceFieldId);
        bool CheckAndUpdateBuildProcess(int villageId);
        bool CheckAndUpdateBuildProcess(Village village);
        List<BuildingRequirement> CheckBuildingRequirements(int villageId, int buildingId);
        List<BuildingRequirement> CheckBuildingRequirements(Village village, int buildingId);
    }
}
