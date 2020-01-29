using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverPage.Model
{
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }
        public string UserName { get; set; }    
        public int BookingId { get; set; }
        public string BicycleNumber { get; set; }
        public int BicycleId { get; set; }
        public string DeliveryAddress { get; set; }
        public double Deliverylat { get; set; }
        public double Deliverylng { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string DeliveryUserName { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime ConfirmationTime { get; set; }
        public  DateTime DeliverTime { get; set; }
        public DateTime Cancelationtime { get; set; }
    }
}
