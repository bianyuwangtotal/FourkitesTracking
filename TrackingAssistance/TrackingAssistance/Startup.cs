using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Quartz;
using Quartz.Impl;

[assembly: OwinStartup(typeof(TrackingAssistance.Startup))]

namespace TrackingAssistance
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            app.MapSignalR();
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
        }
    }
}