using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Resources
{
    public class ResourceFieldRepository : Repository<ResourceField>, IResourceFieldRepository
    {
        private readonly string _properties = "Resource";

        public ResourceFieldRepository(ApplicationDbContext context) : base(context) { }

        public ResourceField GetResourceField(int id)
        {
            return Get(id, _properties);
        }

        public IEnumerable<ResourceField> GetResourceFields(bool enabled = false)
        {
            return GetAll(r => r.Enabled == enabled, includeProperties: _properties);
        }
    }
}
