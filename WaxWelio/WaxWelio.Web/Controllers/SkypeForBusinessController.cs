
using System.Web.Mvc;

namespace WaxWelio.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class SkypeForBusinessController : BaseController
    {
        // GET: SkypeForBusiness
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Call(string patient, string carer, string skype)
        {
            ViewBag.Hospital = BaseApiHeader.HospitalName;
            return View();
        }
    }
}