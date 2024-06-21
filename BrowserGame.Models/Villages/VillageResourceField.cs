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
    public class VillageResourceField : VillageBuildingBase
    {
        public int VillageId { get; set; }

        [ValidateNever]
        public Village Village { get; set; }

        public int ResourceFieldId { get; set; }

        [ValidateNever]
        public ResourceField ResourceField { get; set; }

        public int ResourceId { get; set; }

        [ValidateNever]
        public Resource Resource { get; set; }

        public int ProductionPerHour { get; set; }

        [NotMapped]
        public double RealProductionPerSecond
        {
            get
            {
                return ProductionPerHour / 3600;
            }
        }
    }
}
