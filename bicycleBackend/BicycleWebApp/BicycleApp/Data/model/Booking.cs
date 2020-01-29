using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.Data.model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int BicycleId { get; set; }
        public string UserName { get; set; }
        public string BicycleNumber { get; set; }
        public DateTime DateTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Active { get; set; }
    }
}
