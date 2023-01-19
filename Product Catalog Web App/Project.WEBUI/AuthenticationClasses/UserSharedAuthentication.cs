using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.AuthenticationClasses
{
    public class UserSharedAuthentication : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Admin"] != null || httpContext.Session["Member"] != null || httpContext.Session["Visitor"] != null)
            {
                return true;
            }
            httpContext.Response.Redirect("/Home/Login");
            return false;
        }
    }
}