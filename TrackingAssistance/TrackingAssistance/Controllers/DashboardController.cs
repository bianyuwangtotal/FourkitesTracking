using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingAssistance.Hubs;
using TrackingAssistance.Models;
using TrackingAssistance.TrackingJob;

namespace TrackingAssistance.Controllers
{
    public class DashboardController : Controller
    {
        DBConnections.SqlSrv01DBConnection dbSqlSrv01 = new DBConnections.SqlSrv01DBConnection();

        DBConnections.Sql4DBConnections sql4 = new DBConnections.Sql4DBConnections();


        // GET: Dashboard
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["loginCookie"];

            if (reqCookies == null)
            {
                // already login , go to dashboard directely 
                return RedirectToAction("index", "home");
            }
            else {
                List<LoadDisplay> kraftLoads = dbSqlSrv01.getKraftLoads();
                if (Request.IsAjaxRequest())
                {

                    return PartialView("_TablePartialView", kraftLoads);
                }

                return View(kraftLoads);
            }
        }

    



        public ActionResult ApplyLane()
        {
            List<RunningJob> jobs = new List<RunningJob>();

            var allJobKeys = Globals.Globals.scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.ToList();

            foreach (var v in allJobKeys)
            {
                RunningJob job = new RunningJob();
                job.Bol = v.Name;

                job.Lane = Globals.Globals.scheduler.GetJobDetail(new JobKey(v.Name, "BolNum")).Result.JobDataMap.GetInt("laneId");
                job.position = Globals.Globals.scheduler.GetJobDetail( new JobKey(v.Name, "BolNum")).Result.JobDataMap.GetInt("From");

                jobs.Add(job);
            }


          

            List<FourKites_CustomerLane> laneList = dbSqlSrv01.getLaneList();


            ViewBag.laneId = new SelectList(from d in laneList select new{d.id,laneName =d.id +" - "+ d.CustomerLane},"id", "laneName");

            if (Request.IsAjaxRequest())
            {
              
                return PartialView("_RunningLanePartialView", jobs);

            }
            return View(jobs.ToList());

        }

       
        

        public JsonResult ApplyLaneToBol(string bol,int lane,int from, int to)
        {// create job
                IJobDetail job = JobBuilder.Create<ApplyToBol>()
                .WithIdentity(bol, "BolNum")
                .UsingJobData("laneId",lane)
                .UsingJobData("From",from)
                .UsingJobData("To",to) 
                .Build();

            int repeateTime = to - from + 1;
            // create trigger . in quartz, one trigger one job
            ITrigger trigger = TriggerBuilder.Create()
                        
                            .WithSimpleSchedule(s => s.WithIntervalInSeconds(20).WithRepeatCount(repeateTime))
                            .StartNow()
                            .Build();

            Globals.Globals.scheduler.ScheduleJob(job, trigger);
            Globals.Globals.scheduler.Start();


            return Json(" ", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoadInfor(string bol)
        {
            LoadInfoDisplayModel load = sql4.getLoadInformations(bol);

            string jsonString;
            jsonString = JsonConvert.SerializeObject(load);

            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }


        
        public JsonResult getTotalPings(int laneId) {

            List<FourKites_GPS_Sequencer> pings = dbSqlSrv01.getPingsById(laneId);
         //   int totalNum =dbSqlSrv01.getTotalPingNum(laneId);

            return Json(JsonConvert.SerializeObject(pings), JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateCheck(string bol, string action)
        {   if (dbSqlSrv01.updateCheck(bol, action))
            {
                UpdateHub.updateLoadList();
                return Json(true, JsonRequestBehavior.AllowGet);
             
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}