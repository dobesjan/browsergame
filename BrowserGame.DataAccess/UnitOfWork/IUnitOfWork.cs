using BrowserGame.DataAccess.Repository.Users;
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
    }
}
