using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Resources
{
    public class ResourceFieldResource : Entity
    {
        public int ResourceFieldId { get; set; }

        [ValidateNever]
        public ResourceField ResourceField { get; set; }

        public int ResourceId { get; set; }

        [ValidateNever]
        public Resource Resource { get; set; }

        public int CostId { get; set; }

        [ForeignKey(nameof(CostId))]
        public Cost Cost { get; set; }
    }
}
