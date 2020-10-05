using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Quartz;

namespace TrackingAssistance.Hubs
{
    public class UpdateHub : Hub
    {

        public static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<UpdateHub>();

        public static void updateLoadList()
        {
            hubContext.Clients.All.updateUserList();
        }

        public static void updateRunningLaneList()
        {
            hubContext.Clients.All.updateRunningLaneList();
        }

        public static void RunningLaneEnd(JobKey jobKey)
        {

            Globals.Globals.scheduler.DeleteJob(jobKey);
          //  hubContext.Clients.All.updateLastPingColor(jobKey);

        }

        internal static void endJobAndUpdate(JobKey jobKey)
        {
             Globals.Globals.scheduler.DeleteJob(jobKey);
             hubContext.Clients.All.updateRunningLaneList();
        }
    }
}

