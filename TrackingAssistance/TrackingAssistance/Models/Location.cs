using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAssistance.Models
{
    public class Location
    {
        public string shipper { get; set; }
        public string billOfLading { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }



        public void getshipper()
        {
            switch (billOfLading.Substring(0, 1))

            {
      
                case "2":
                    shipper = "Kraft";
                    break;
                case "7":
                    shipper = "Mondelez";
                    break;
             

            }



        }
    }
}