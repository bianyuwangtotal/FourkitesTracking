using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackingAssistance.Models;

namespace TrackingAssistance.DBConnections
{
    public class SqlSrv01DBConnection
    {
        public SqlSrv01Entities dbSqlSrv01 { get; set; }


        public SqlSrv01DBConnection()
        {

            dbSqlSrv01 = new SqlSrv01Entities();
        }

        public int getUserType(string userName)
        {
            return dbSqlSrv01.TrackingAssistanceUsers.Where(u => u.UserName.Equals(userName)).FirstOrDefault().IsAdmin;

        }

        public bool isUserHaveRight(string userName)
        {
           TrackingAssistanceUser user = dbSqlSrv01.TrackingAssistanceUsers.Where(u => u.UserName.Equals(userName)).FirstOrDefault();
            if (user != null)
                return true;
            return false;
        }

    }
}