using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.BusinessLayer.Exceptions
{
    public class GameException : Exception
    {
        public GameException(string? message) : base(message) 
        {
        }
    }
}
