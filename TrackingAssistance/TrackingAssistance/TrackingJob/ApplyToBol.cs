using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TrackingAssistance.Hubs;
using TrackingAssistance.Models;

namespace TrackingAssistance.TrackingJob
{
    [PersistJobDataAfterExecution]
    public class ApplyToBol : IJob
    {
        private DBConnections.SqlSrv01DBConnection dbsqlsrv01 = new DBConnections.SqlSrv01DBConnection();
        private FourkiteClient fourkiteClient = new FourkiteClient();
   
        public async Task Execute(IJobExecutionContext context)
        {
            string bol = context.JobDetail.Key.Name;
           int lane = context.JobDetail.JobDataMap.GetInt("laneId");
            int from = context.JobDetail.JobDataMap.GetInt("From");
            int to = context.JobDetail.JobDataMap.GetInt("To");

            if (from <= to)
            {  // find ping 

                try
                {
                    
                    FourKites_GPS_Sequencer ping = dbsqlsrv01.dbSqlSrv01.FourKites_GPS_Sequencer.Where(l => l.CustomerLaneId == lane && l.Sequence == from).FirstOrDefault();


                    // send to fourkites
                    if (fourkiteClient.AssignLocation(bol, ping) == 200)
                    {
                        UpdateHub.updateRunningLaneList();                      
                        context.JobDetail.JobDataMap["From"] = ++ from;
                    }
                    else
                    {

                        //sent fail, need to show error message to user

                        //update interface

                    }
                }
                catch (Exception e)
                {

                    var t = e.Message;
                }
            }
            else
            {
                //update the status on the interface
                JobKey jobKey = context.JobDetail.Key;


                //send key to hub and remove this job

                UpdateHub.endJobAndUpdate(jobKey);
            }

            }
                }
}