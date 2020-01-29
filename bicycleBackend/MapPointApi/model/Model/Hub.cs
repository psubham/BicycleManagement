using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MapPointApi.model
{
    public class Hub
    {
        [Key]
        public int Id { get; set; }
        public int Postal { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Sub_locality_l2 { get; set; }
        public string Sub_locality_l1 { get; set; }
        public string Locality { get; set; }
        public string Country { get; set; }
        public string Short_sub_locality_l2 { get; set; }
        public string Short_sub_locality_l1 { get; set; }
        public string Short_locality { get; set; }
        public string Short_country { get; set; }

    }
}
