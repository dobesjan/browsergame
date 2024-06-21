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

        public double Coefficient { get; set; }

        public int GetCost(int level)
        {
            // Use an exponential formula to calculate the cost
            // Cost = BaseCost * (Coefficient ^ (level - 1))
            return (int)(BaseCost * Math.Pow(Coefficient, level - 1));
        }
    }
}
