using BrowserGame.DataAccess.Data;
using BrowserGame.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository.Users
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext db) : base(db)
        {
        }

        public Player GetByUserId(string userId)
        {
            return Get(p => p.UserId == userId);
        }
    }
}
