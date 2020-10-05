using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackingAssistance.Models;

namespace TrackingAssistance.DBConnections
{
    public class Sql4DBConnections
    {
        public Sql4Entities Sql4{ get; set; }
        public SqlSrv01Entities Sqlsrv01 { get; set; }

        public Sql4DBConnections()
        {

            Sqlsrv01 = new SqlSrv01Entities();
            Sql4 = new Sql4Entities();

        }
        public int getLaneId(string bol,int leg)
        {
            int laneId = 0;
           probill p = Sql4.probills.Where(l => l.bill_of_lading.Equals(bol) && l.leg == leg).FirstOrDefault();
           FourKites_CustomerLane lane = Sqlsrv01.FourKites_CustomerLane.Where(l => l.PickLocation == p.pick_up && l.DelLocation == p.deliver).FirstOrDefault();
            if (lane == null)
            {
                lane = Sqlsrv01.FourKites_CustomerLane.Where(l => l.PickLocation == p.deliver && l.DelLocation == p.pick_up).FirstOrDefault();
                if (lane != null)
                    laneId = -lane.id;

            }
            else
                laneId = lane.id;

            return laneId;
        }


        public LoadInfoDisplayModel getLoadInformations(string bol) {
            

            LoadInfoDisplayModel load = new LoadInfoDisplayModel();
         
                List<Leg> legs = Sql4.probills.Where(p => p.bill_of_lading.Equals(bol))
                    .Join(Sql4.companies, p => p.pick_up, c => c.company_id,
                    (p, c) => new { pro = p, pickup = c })
                    .Join(Sql4.companies, proPick => proPick.pro.deliver, c => c.company_id,
                    (proPick, c) => new { proPickup = proPick, deliver = c }
                    )
                    .Select(L => new Leg
                    {
                        LegNum = (int)L.proPickup.pro.leg,
                        DeliveryCompany = L.deliver.company_name,
                        PickupCompany = L.proPickup.pickup.company_name,
                       
                    }).ToList();

                foreach (Leg l in legs)
                {
                    l.EstLaneNum = getLaneId(bol, l.LegNum);
                    load.legs.Add(l);
                }
                load.BolNum = bol;
            
       
          
            return load;    
                }
            
    }
}