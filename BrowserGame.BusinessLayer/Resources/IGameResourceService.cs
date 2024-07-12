using BrowserGame.Models.Buildings;
using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Resources
{
    public interface IGameResourceService
    {
        void InitVillageResources(int villageId);
        void InitResourceFields(int villageId);
        void CreateVillageWithResourceInit(int playerId);

        //Calculates production in timeframe between last calculation and now
        void CalculateProductionForVillage(int villageId);

        bool HasEnoughResources(int villageId, int level, int buildingId);
        bool HasEnoughResources(Village village, int level, int buildingId);
    }
}
