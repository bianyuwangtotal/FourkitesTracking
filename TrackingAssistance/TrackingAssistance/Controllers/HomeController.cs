using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.DirectoryServices.AccountManagement;
using System.Resources;
using System.Data;
using System.Threading;
using System.Globalization;

using System.Data.SqlClient;

namespace TrackingAssistance.Controllers
{
    public class HomeController : Controller
    {
        DBConnections.SqlSrv01DBConnection dbSqlSrv01 = new DBConnections.SqlSrv01DBConnection();

        public ActionResult Index()
        {
            

            HttpCookie reqCookies = Request.Cookies["loginCookie"];

            if (reqCookies == null )
            {
                // already login , go to dashboard directely 
                return View();
              
            }

            

           return RedirectToAction("index", "Dashboard"); 
          


        }

        public ActionResult Logout()
        {
            HttpCookie reqCookies = Request.Cookies["loginCookie"];

            if (reqCookies != null)
            {
                reqCookies["userName"] = "";
                reqCookies.Expires = DateTime.Now.AddYears(-1);
                Request.Cookies.Add(reqCookies);
                Request.Cookies.Clear();
            
            }
            return RedirectToAction("index");

        }
        public JsonResult Login(string userName, string password)
        {
            int loginStatus = 1;
            if (Membership.ValidateUser(userName, password) is true)
            { 
                if (dbSqlSrv01.isUserHaveRight(userName))
                { 
                var domainContext = new PrincipalContext(ContextType.Domain, "totallogistics");
                var currentADUser = UserPrincipal.FindByIdentity(domainContext, userName);

                
                HttpCookie loginCookie = new HttpCookie("loginCookie");
                loginCookie["userName"] = currentADUser.DisplayName;            
                loginCookie["userType"] = dbSqlSrv01.getUserType(userName).ToString();
                loginCookie.Expires= DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(loginCookie);
                loginStatus = 0;
                }
            }
            return Json(loginStatus, JsonRequestBehavior.AllowGet);
        }
    }
}
