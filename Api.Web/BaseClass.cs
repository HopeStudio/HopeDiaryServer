using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Web
{
    public class BaseClass
    {
        public bool CheckLogin(HttpContext context)
        {
            if (context.Session["user"] == null)
            {
                return false;
            }

            return true;
        }
    }
}