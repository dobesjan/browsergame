using BrowserGame.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Resources
{
    public interface IResourceFieldRepository : IRepository<ResourceField>
    {
        ResourceField GetResourceField(int id);
        IEnumerable<ResourceField> GetResourceFields(bool enabled = false);
    }
}
