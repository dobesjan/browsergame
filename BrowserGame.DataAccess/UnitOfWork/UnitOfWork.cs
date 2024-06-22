using BrowserGame.DataAccess.Data;
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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public IPlayerRepository PlayerRepository { get; }
        public IVillageRepository VillageRepository { get; }
        public IResourceRepository ResourceRepository { get; }
        public IVillageResourceRepository VillageResourceRepository { get; }
        public IResourceFieldRepository ResourceFieldRepository { get; }
        public IVillageResourceFieldRepository VillageResourceFieldRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            PlayerRepository = new PlayerRepository(context);
            VillageRepository = new VillageRepository(context);
            ResourceRepository = new ResourceRepository(context);
            VillageResourceRepository = new VillageResourceRepository(context);
            ResourceFieldRepository = new ResourceFieldRepository(context);
            VillageResourceFieldRepository = new VillageResourceFieldRepository(context);
        }
    }
}
