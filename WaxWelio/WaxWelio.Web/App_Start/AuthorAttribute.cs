using System;
using System.Web.Mvc;
using WaxWelio.Common.Config;

namespace WaxWelio.Web
{
    public class AuthorAttribute: AuthorizeAttribute
    {
        public string RedirectUrl { get; set; } = GlobalConstant.UrlLogin;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                         || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (skipAuthorization)
            {
                var cachePolicy =
                   filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                return;
            }
            // This code added to support custom Unauthorized pages.
            if (filterContext.HttpContext.Request.Cookies[CookieConstant.ApiHeader] == null)
            {
                if (RedirectUrl != null)
                    filterContext.Result = new RedirectResult(RedirectUrl);
                else
                // Redirect to Login page.
                    HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var cachePolicy =
                    filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            }
        }
    }
}