using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackingAssistance.Models;
using TrackingAssistance.Utils;

namespace TrackingAssistance.DBConnections
{
    public class SqlSrv01DBConnection
    {
        public SqlSrv01Entities dbSqlSrv01 { get; set; }
        private Sql4DBConnections sql4DB { get; set; }

        public SqlSrv01DBConnection()
        {

            dbSqlSrv01 = new SqlSrv01Entities();
            sql4DB = new Sql4DBConnections();

        }

        public int getUserType(string userName)
        {
            return dbSqlSrv01.TrackingAssistanceUsers.Where(u => u.UserName.Equals(userName)).FirstOrDefault().IsAdmin;

        }
        public List<FourKites_CustomerLane> getLaneList() {
            List<FourKites_CustomerLane> laneList = new List<FourKites_CustomerLane>();
            laneList = dbSqlSrv01.FourKites_CustomerLane.OrderBy(l=>l.id).ToList();

            return laneList;
        }
        public List<LoadDisplay> getKraftLoads()
        {

            List<LoadDisplay> loads = new List<LoadDisplay>();

            List<FourkitesTrackingSentLeg> pickupList =  dbSqlSrv01.FourkitesTrackingSentLegs.Where(l => l.leg == 1 && StringConfig.kraftList.Contains((int)l.customer_id) && l.current_status.Equals("PICKED UP")&& l.is_checked == 1).ToList();
            List<backupLoad> deliveredList = dbSqlSrv01.backupLoads.Where(l => l.is_checked == 1 && l.leg == 1 && l.bol_number.StartsWith("2")).ToList();

            var sentMapper = LoadMappers.DBmapper.sentToDisplay.CreateMapper();
            var backupMapper = LoadMappers.DBmapper.backupToDisplay.CreateMapper();

            foreach (FourkitesTrackingSentLeg l in pickupList)
            {
                LoadDisplay load = sentMapper.Map <LoadDisplay>(l);
                loads.Add(load);
            }


            foreach (backupLoad l in deliveredList)
            {
                LoadDisplay load = backupMapper.Map<LoadDisplay>(l);
                loads.Add(load);
            }

            return loads;


        }

        internal List<FourKites_GPS_Sequencer> getPingsById(int laneId)
        {
           return dbSqlSrv01.FourKites_GPS_Sequencer.Where(l => l.CustomerLaneId == laneId).OrderBy(l=>l.Sequence).ToList();
        }

        //internal LoadInfoDisplayModel getLoadInfor(string bol)
        //{
        //    LoadInfoDisplayModel loadInfo = new LoadInfoDisplayModel();
        //    loadInfo = sql4DB.getLoadInformations(bol);
        //    foreach (Leg l in loadInfo.legs)
        //    {
        //        int laneNum =dbSqlSrv01.FourKites_CustomerLane.Where(l=>l.PickLocation)
        //    }


        //    return loadInfo;
        //}

        internal int getTotalPingNum(int laneId)
        {
            int total = dbSqlSrv01.FourKites_GPS_Sequencer.Where(l => l.CustomerLaneId == laneId).Count();
            return total;
        }

        public bool isUserHaveRight(string userName)
        {
           TrackingAssistanceUser user = dbSqlSrv01.TrackingAssistanceUsers.Where(u => u.UserName.Equals(userName)).FirstOrDefault();
            if (user != null)
                return true;
            return false;
        }

        public bool updateCheck(string bol, string action)
        {
            if (bol.Equals(null))
            {
                return false;
            }
            else
            {
                if (action.Equals("P"))
                {

                  List<FourkitesTrackingSentLeg> loads = dbSqlSrv01.FourkitesTrackingSentLegs.Where(l => l.bol_number.Equals(bol)).ToList();
                    if (loads.Count != 0)
                    {
                        foreach (FourkitesTrackingSentLeg l in loads)
                        {
                            l.is_checked = 0;
                        }
                      

                    }
                    else return false;
                }
                else if (action.Equals("D"))
                {
                    List<backupLoad> loads = dbSqlSrv01.backupLoads.Where(l => l.bol_number.Equals(bol)).ToList();
                    if (loads.Count != 0)
                    {
                        foreach (backupLoad l in loads)
                        {
                            l.is_checked = 0;
                        }
                    }
                    else return false;
                }
                else
                    return false;

                dbSqlSrv01.SaveChanges();
                return true;
            }
          
        }

    }
}