using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WaxWelio.Common.Config;
using WaxWelio.Common.Enum;
using WaxWelio.Common.LanguageHelper;
using WaxWelio.Common.Object;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class BaseController : Controller
    {
        protected ApiHeader BaseApiHeader;

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            var cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            //else
            //  cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = LanguageHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }

        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);
        //    var controller = filterContext.RouteData.Values["controller"];
        //    var httpCookie = HttpContext.Request.Cookies[CookieConstant.ApiHeader];
        //    ViewBag.BaseUserType = 0;
        //    ViewBag.BaseUserRole = 0;
        //    ViewBag.IsHospital = false;
        //    if (httpCookie != null)
        //    {
        //        try
        //        {
        //            BaseApiHeader = JsonConvert.DeserializeObject<ApiHeader>(httpCookie.Value);
        //            if (BaseApiHeader.User != null)
        //            {
        //                BaseApiHeader.User.FirstName = BaseApiHeader.User.FirstName;
        //                BaseApiHeader.User.LastName = BaseApiHeader.User.LastName;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            httpCookie.Expires = DateTime.Now.AddDays(-1);
        //            HttpContext.Response.Cookies.Add(httpCookie);
        //        }
                
        //    }

        //    if (BaseApiHeader == null) return;
        //    ViewBag.BaseUserType = BaseApiHeader.UserType;
        //    ViewBag.BaseUserRole = BaseApiHeader?.HospitalRoles;
        //    if (!string.IsNullOrEmpty(BaseApiHeader.HospitalName))
        //    {
        //        BaseApiHeader.HospitalName = BaseApiHeader.HospitalName;
        //    }
            
        //    if (!string.IsNullOrEmpty(BaseApiHeader.HospitalSelected))
        //        ViewBag.IsHospital = true;
        //    if (BaseApiHeader.UserType != (int)UserType.SystemAdmin) return;
        //    if (!ListPermission.SystemAdmin.Contains(controller) && !ViewBag.IsHospital)
        //        filterContext.Result = RedirectToAction("ActiveClinics", "Clinic");
        //}

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            // Redirect on error:
            //filterContext.Result = RedirectToAction("Index", "Home");
        }

        protected static string ParseStrDate(string value)
        {
            var ts = ParseDouble(value);
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts).ToLocalTime();
            return result.ToString("dd-MM-yyyy");
        }

        protected static bool ParseBool(string value)
        {
            int result;
            if (string.IsNullOrWhiteSpace(value) || !int.TryParse(value, out result))
            {
                throw new Exception();
            }
            return result == 1;
        }

        protected static double ParseDouble(string value)
        {
            double result;
            if (string.IsNullOrWhiteSpace(value) || !double.TryParse(value, out result))
            {
                throw new Exception();
            }
            return result;
        }
    }
}