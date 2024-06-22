using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Villages
{
    public class VillageResourceRepository : Repository<VillageResource>, IVillageResourceRepository
    {
        public VillageResourceRepository(ApplicationDbContext context) : base(context) { }
    }
}
