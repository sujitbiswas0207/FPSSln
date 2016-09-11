using FPS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace FPS.Common
{
    public static class CacheManager
    {
        public static void LoadSystemDetails()
        {
            GetSystemMesssage();
        }

        private const string Userkey = "Userkey";

        public static AspNetUser UserData
        {
            get { return (AspNetUser)HttpContext.Current.Session[Userkey]; }
            set { HttpContext.Current.Session[Userkey] = value; }
        }

        public static void GetSystemMesssage()
        {
            //var xmlDoc = XElement.Load(HttpContext.Current.Server.MapPath(@"~/App_Data/SystemLabelConfigurations.xml"));
            //SystemMessageData = (from xmlMsg in xmlDoc.Descendants("Messages")
            //                     select new SystemMessageCached
            //                     {
            //                         SaveMessage = xmlMsg.Element("SaveMessage").Value,
            //                         DeleteMessage = xmlMsg.Element("DeleteMessage").Value,
            //                         ErrorMessage = xmlMsg.Element("ErrorMessage").Value,
            //                         ImportMessage = xmlMsg.Element("ImportMessage").Value,
            //                         ImportSuccessMessage = xmlMsg.Element("ImportSuccessMessage").Value,
            //                         Inuse = xmlMsg.Element("Inuse").Value,
            //                     }).FirstOrDefault();
        }

        public static void RenewCurrentUser()
        {
            System.Web.HttpCookie authCookie =
                System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = null;
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket != null && !authTicket.Expired)
                {
                    FormsAuthenticationTicket newAuthTicket = authTicket;

                    if (FormsAuthentication.SlidingExpiration)
                    {
                        newAuthTicket = FormsAuthentication.RenewTicketIfOld(authTicket);
                    }
                    string userData = newAuthTicket.UserData;
                    string[] roles = userData.Split(',');

                    System.Web.HttpContext.Current.User =
                        new System.Security.Principal.GenericPrincipal(new FormsIdentity(newAuthTicket), roles);
                }
            }
        }

        public static void LogoffCurrentUser()
        {
            //System.Web.HttpCookie authCookie = null;
            //FormsAuthenticationTicket newAuthTicket=null;
            //System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(newAuthTicket), null);

            FormsAuthentication.SignOut();
            HttpContext.Current.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);


            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}