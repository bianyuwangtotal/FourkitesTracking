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
            ViewBag.Title = "Login";

            HttpCookie reqCookies = Request.Cookies["loginCookie"];
            if (reqCookies != null)
            {
                // already login , go to dashboard directely 
                return RedirectToAction("index", "home");
            }
        
            return View();

         
        }


        public JsonResult Login(string userName, string password)
        {
            string message= "not allowed";
            if (Membership.ValidateUser(userName, password) is false)
            {
                //- In case username or password is wrong, re-load Login View passing back the model
                //so the framework can make the "bad" username and pass "sticky"

                message = "not allowed";

            }
            else {

                if (dbSqlSrv01.isUserHaveRight(userName))
                { 
                var domainContext = new PrincipalContext(ContextType.Domain, "totallogistics");
                var currentADUser = UserPrincipal.FindByIdentity(domainContext, userName);

                
                HttpCookie loginCookie = new HttpCookie("loginCookie");
                loginCookie["userName"] = currentADUser.DisplayName;
                loginCookie["userCompany"] = "totallogistics";
                loginCookie["userType"] = dbSqlSrv01.getUserType(userName).ToString();
                loginCookie.Expires.AddMinutes(10);
                Response.Cookies.Add(loginCookie);

                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
