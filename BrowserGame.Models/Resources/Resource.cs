using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.Models.Resources
{
    public class Resource : Entity
    {
        [Required]
        public string Name { get; set; }

        public bool Enabled { get; set; }
    }
}
