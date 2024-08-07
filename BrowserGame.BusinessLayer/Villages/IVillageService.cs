﻿using BrowserGame.Models.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Villages
{
    public interface IVillageService
    {
        Village GetVillage(int villageId);
        Village GetVillage(int villageId, int playerId);
        Village CreateVillage(int playerId);
        double GetEffectValue(int villageId, int effectId);
    }
}
