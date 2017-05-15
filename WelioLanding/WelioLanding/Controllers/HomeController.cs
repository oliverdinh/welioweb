using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace WelioLanding.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            return View();
        }

  
        public ActionResult SendEmail(FormCollection fc)
        {
            var email = fc["Email"];
            var smtpClient = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("oliver.dinh@welio.com", "Welio2017")
            };

            var mail = new MailMessage { From = new MailAddress("oliver.dinh@welio.com", "(Welio Pty Ltd)") };

            mail.To.Add(new MailAddress("wing.yip@welio.com"));
            mail.Subject = "Welio - Register";
            var content = @"<p>Dear,</p>
<table style=""border-spacing: 0px;padding-left: 0px;width: 300px;"">
    <tbody>
    <tr>
        <td style=""margin-right: -14px;"">
            First Name:
        </td>
        <td>
           {0}
        </td>
    </tr>
    <tr>
        <td>
            Last Name:
        </td>
        <td>
            {1}
        </td>
    </tr>
    <tr>
        <td>
            Email:
        </td>
        <td>
            {2}
        </td>
    </tr>
    </tbody>
</table>
<p>Regards,</p>";
            mail.Body = string.Format(content, fc["FirstName"], fc["LastName"], fc["Email"]);
            mail.IsBodyHtml = true;
            smtpClient.Send(mail);

            return Json(new { name=fc["FirstName"] });
        }
    }
}
