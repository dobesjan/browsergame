﻿using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Villages
{
    public interface IVillageRepository : IRepository<Village>
    {
        Village GetVillage(int id);
        Village GetVillage(int villageId, int playerId);
    }
}
