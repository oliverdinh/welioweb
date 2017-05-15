using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.Object;

namespace WaxWelio.Web
{
    public class AuthorCustomAttribute : AuthorizeAttribute
    {
        private readonly UserRole[] _roles;

        public AuthorCustomAttribute(params UserRole[] roles)
        {
            _roles = roles;
        }

        public string RedirectUrl { get; set; } = GlobalConstant.UrlLogin;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                                    ||
                                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                                        typeof(AllowAnonymousAttribute), true);
            if (skipAuthorization)
            {
                var cachePolicy =
                    filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                return;
            }
            var cookie = filterContext.HttpContext.Request.Cookies[CookieConstant.ApiHeader];
            // This code added to support custom Unauthorized pages.
            if (cookie == null)
            {
                if (RedirectUrl != null)
                {
                    filterContext.Result = new RedirectResult(RedirectUrl);
                    //throw new Exception(nameof(filterContext));
                }
                    
                else
                    // Redirect to Login page.
                    HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var apiHeader = JsonConvert.DeserializeObject<ApiHeader>(cookie.Value);
                var listRole = _roles.Select(x => (int) x).ToList();
                if (apiHeader.UserType == 9)
                {
                }
                else
                {
                    if (listRole.Count == 0)
                    {
                        var cachePolicy =
                            filterContext.HttpContext.Response.Cache;
                        cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                    }
                    else
                    {
                        if (apiHeader.HospitalRoles.Any(x => listRole.Contains(x)))
                        {
                            var cachePolicy =
                                filterContext.HttpContext.Response.Cache;
                            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                        }
                        else
                        {
                            filterContext.Result = new RedirectResult("~/Home");
                        }
                    }
                }
            }
        }
    }
}