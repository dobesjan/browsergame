using BrowserGame.Models.Buildings;
using BrowserGame.Models.Villages;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Resources
{
    public class ResourceField : BuildingBase
    {
        [ValidateNever]
        public List<VillageResourceField> VillageFields { get; set; }

        [ValidateNever]
        public List<ResourceFieldResource> ResourceFieldResources { get; set; }

        public int ResourceId { get; set; }

        [ValidateNever]
        public Resource Resource { get; set; }
    }
}
