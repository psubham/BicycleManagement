using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.Data.model
{
    public class Bicycles
    {
        [Key]
        public int BicycleId { get; set; }
        [ForeignKey("BicycleType")]
        public int TypeId { get; set; }
        [ForeignKey("Hub")]
        public int HubId { get; set; }
        public bool IsRent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string BicycleNumber { get; set; }

    }
}
