using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Villages
{
    public class VillageRepository : Repository<Village>, IVillageRepository
    {
        private readonly string _properties = "VillageResources.Village,VillageResources.Resource,VillageFields.Village,VillageFields.ResourceField";

        public VillageRepository(ApplicationDbContext context) : base(context) { }

        public Village GetVillage(int id)
        {
            return Get(id, includeProperties: _properties);
        }

        public Village GetVillage(int villageId, int playerId)
        {
            return Get(v => v.Id == villageId && v.PlayerId == playerId, includeProperties: _properties);
        }
    }
}
