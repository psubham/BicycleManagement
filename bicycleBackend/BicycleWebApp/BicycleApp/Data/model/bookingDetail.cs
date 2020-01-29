using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.Data.model
{
    public class BookingDetail
    {
        public int TypeId { get; set; }
        public string UserName { get; set; }
        public int HubId { get; set; }
        public  string BicycleNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DeliveryAddress { get; set; }

    }
}
