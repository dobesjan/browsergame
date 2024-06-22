using BrowserGame.Models.Resources;
using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Villages
{
    public interface IVillageResourceRepository : IRepository<VillageResource>
    {
        /*
        VillageResourceField GetVillageResource(int id);
        IEnumerable<VillageResourceField> GetVillageResources(int villageId, bool enabled = false);
        */
    }
}
