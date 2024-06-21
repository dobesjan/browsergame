﻿using BrowserGame.DataAccess.Data;
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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            PlayerRepository = new PlayerRepository(context);
            VillageRepository = new VillageRepository(context);
        }
    }
}