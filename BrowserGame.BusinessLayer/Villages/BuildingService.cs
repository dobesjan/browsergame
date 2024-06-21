using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Villages
{
    public class BuildingService : IBuildingService
    {
        public void AddBuildOrder(int villageId, int slotId, int buildingId)
        {
            //TODO: Consider how to deal with slots

            // Get VillageBuilding by slot id

            // Check if village has enough resources

            // Check if build queue is not full

            // Add to build queue
        }

        public void AddLevelUpOrder(int villageId, int villageBuilding)
        {
            //TODO: Consider how to deal with slots

            // Get VillageBuilding by slot id

            // Check if village has enough resources

            // Check if build queue is not full

            // Add to build queue
        }

        public void AddLevelUpResourceFieldOrder(int villageId, int villageResourceFieldId)
        {
            // Get resource field by id

            // Check if village has enough resources

            // Check if build queue is not full

            // Add to build queue
        }

        public void CheckAndUpdateBuildProcess(int villageId)
        {
            // Get all build queue items for village

            // Check if some of them are completed

            // If completed then remove them from build queue and increase building and resource field levels
        }
    }
}
