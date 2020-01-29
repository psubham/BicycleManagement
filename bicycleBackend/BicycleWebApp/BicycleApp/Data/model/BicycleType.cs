using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.Data.model
{
    public class BicycleType
    {
        [Key]
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

    }
}
