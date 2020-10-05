using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAssistance.Globals
{
    public class Globals
    {
        public static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
    }
}