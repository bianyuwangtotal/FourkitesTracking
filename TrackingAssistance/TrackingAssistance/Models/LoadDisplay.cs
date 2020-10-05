using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAssistance.Models
{
    public class LoadDisplay
    {
        public string BolNum { get; set; }
        public string Status { get; set; }
        public DateTime StatusTime { get; set; }
        public bool IsChecked { get; set; }
    }
}