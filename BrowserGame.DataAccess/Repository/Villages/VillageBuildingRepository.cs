using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Villages
{
    public class VillageBuildingRepository : Repository<VillageBuilding>, IVillageBuildingRepository
    {
        public VillageBuildingRepository(ApplicationDbContext context) : base(context) { }
    }
}
