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

        //Calculates production in timeframe between last calculation and now
        void CalculateProductionForVillage(int villageId);
    }
}
