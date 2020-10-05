using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAssistance.App_Start
{
    public class StringConfig
    {
        public static IReadOnlyList<int> customerList = new List<int> { 25051, 25052, 20027, 19654, 45858, 45859, 45861, 45860, 63182 };

     //   static string FOURKITES_API_SECRET = "z3avtexrkzvbby9ldg9rbdjcshloufp5yuq1whdimmyzcgrdanjnbdn5qwi1buh6ouvirff1kznbcuozrlldvnhzrt0=";
      public static string auth = "bW1hcndhbkB0b3RhbGxvZ2lzdGljcy5jb206UmVnZ2llIzky"; //stageing
       // public static string auth = "cmxhdXpvbkB0b3RhbGxvZ2lzdGljcy5jb206UmVnZ2llIzky";

     //   public static string assignmentLocation = "https://tracking-api.fourkites.com/api/v1/tracking/locations";
     
      public static string assignmentLocation = "https://tracking-api-staging.fourkites.com/api/v1/tracking/locations";


    }
}