using BrowserGame.Models.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Villages
{
    public class VillageResource : Entity
    {
        public int VillageId { get; set; }

        [ValidateNever]
        public Village Village { get; set; }

        public int ResourceId { get; set; }

        [ValidateNever]
        public Resource Resource { get; set; }

        public double RealAmount { get; set; }

        [NotMapped]
        public int Amount
        {
            get
            {
                return (int) Math.Floor(RealAmount);
            }
        }

        public DateTime LastAmountCalculation { get; set; }

        [NotMapped]
        public TimeSpan AmountCalculationDifference
        {
            get
            {
                return DateTime.UtcNow.Subtract(LastAmountCalculation);
            }
        }

        public void AddAmount(double amount, double capacity)
        {
            RealAmount = Math.Min(RealAmount + amount, capacity);
        }
    }
}
