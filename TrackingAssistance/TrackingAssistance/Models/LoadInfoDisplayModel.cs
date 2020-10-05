using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAssistance.Models
{
    public class LoadInfoDisplayModel
    {
        public string BolNum { get; set; }
        public List<Leg> legs { get; set; }
        public LoadInfoDisplayModel()
        {
            legs = new List<Leg>();
        }

    }
}