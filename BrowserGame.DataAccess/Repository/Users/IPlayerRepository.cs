using BrowserGame.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Users
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player GetByUserId(string userId);
    }
}
