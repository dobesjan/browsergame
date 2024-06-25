﻿using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Buildings
{
    public class BuildingBaseRepository : Repository<BuildingBase>, IBuildingBaseRepository
    {
        public BuildingBaseRepository(ApplicationDbContext context) : base(context) { }
    }
}
