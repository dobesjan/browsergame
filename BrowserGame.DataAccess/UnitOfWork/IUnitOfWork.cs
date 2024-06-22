using BrowserGame.DataAccess.Repository.Resources;
using BrowserGame.DataAccess.Repository.Users;
using BrowserGame.DataAccess.Repository.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPlayerRepository PlayerRepository { get; }
        IVillageRepository VillageRepository { get; }
        IResourceRepository ResourceRepository { get; }
        IVillageResourceRepository VillageResourceRepository { get; }
        IResourceFieldRepository ResourceFieldRepository { get; }
        IVillageResourceFieldRepository VillageResourceFieldRepository { get; }
    }
}
