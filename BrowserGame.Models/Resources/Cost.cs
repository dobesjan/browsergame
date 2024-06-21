using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Resources
{
    public class Cost : Entity
    {
        public int BaseCost { get; set; }

        public int Coefficient { get; set; }

        //TODO: Consider cost equation for general purposes
    }
}
