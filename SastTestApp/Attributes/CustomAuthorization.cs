using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SastTestApp.Attributes
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase context)     
        {
            if (HttpContext.Current.Session["loggedInUser"]==null|| HttpContext.Current.Session["loggedInUser"].ToString()!="jagrti")
            {
                return false;
            }
            return true;
        }
    }
}