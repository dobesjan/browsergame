using BrowserGame.Models.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Villages
{
    public class VillageResourceField
    {
        public int VillageId { get; set; }

        [ValidateNever]
        public Village Village { get; set; }

        public int ResourceFieldId { get; set; }

        [ValidateNever]
        public ResourceField ResourceField { get; set; }
    }
}
