using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapPointApi.model
{
    public class Map
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public  Map()
        {
            lat = 0.0;
            lng = 0.0;
        }
        public  Map(double lat,double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
    }
}
