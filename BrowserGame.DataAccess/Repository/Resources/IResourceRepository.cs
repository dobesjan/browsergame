using BrowserGame.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Resources
{
    public interface IResourceRepository : IRepository<Resource>
    {
        public Resource GetResource(int id);
        public IEnumerable<Resource> GetResources(bool enabled = false);
    }
}
