using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SSRS.Common
{
    public static class ClientConfiguration
    {
        public static string WaitMessage = "Please wait while data is loading...";
        public static string ErrorMessage = "Error occurred while processing your request. Please contact administrator for further assistance.<br/>";
        public static string AuthorizeMessage = "You are not Authorize to view this page. Please contact administrator for further assistance.<br/>";
        public static string WebServiceUserName = @"yourname";
        public static string WebServicePWD = @"yourname";
        public static string WebServiceUserDomain = @"yourname";
        public static string DeliverablesWebSite = System.Configuration.ConfigurationManager.AppSettings["DeliverablesUrl"];
        public static string DeliverableSiteURL
        {
            get
            {
                return ConfigurationManager.AppSettings["DeliverableSiteURL"].ToString();
            }
        }
        public static string NotificationEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["NotificationEmail"].ToString();
            }
        }

        public static string SSRSSite
        {
            get
            {
                return ConfigurationManager.AppSettings["SSRSSite"].ToString();
            }
        }
      
     
        public enum SiteUserRole
        {
            Admin,
            Contributor,
            User
        }

        public enum Portals
        {
            OpSum,
            ControlCenter
        }
    }
}