using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Villages
{
    public class VillageResourceFieldRepository : Repository<VillageResourceField>, IVillageResourceFieldRepository
    {
        private readonly string _properties = "";

        public VillageResourceFieldRepository(ApplicationDbContext context) : base(context) 
        { 
        }

        public VillageResourceField GetVillageResource(int id)
        {
            return Get(id, _properties);
        }

        public IEnumerable<VillageResourceField> GetVillageResources(int villageId, bool enabled = false)
        {
            return GetAll(vr => vr.VillageId == villageId);
        }
    }
}
