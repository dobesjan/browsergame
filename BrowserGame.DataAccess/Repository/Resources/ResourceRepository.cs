using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Resources
{
    public class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        private readonly string _properties = "CapacityEffect";

        public ResourceRepository(ApplicationDbContext context) : base(context) { }

        public Resource GetResource(int id)
        {
            return Get(id, _properties);
        }

        public IEnumerable<Resource> GetResources(bool enabled = false)
        {
            return GetAll(r => r.Enabled == enabled, includeProperties: _properties);
        }
    }
}
