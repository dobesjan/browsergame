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
        void AddLevelUpOrder(int villageId, int villageBuilding);
        void AddLevelUpResourceFieldOrder(int villageId, int villageResourceFieldId);
        bool CheckAndUpdateBuildProcess(int villageId);
        bool CheckAndUpdateBuildProcess(Village village);
    }
}
