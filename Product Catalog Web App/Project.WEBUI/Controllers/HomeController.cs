using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.WEBUI.VMClasses;
using System;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Project.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRepository<AppUser> _auRep;

        public HomeController()
        {
            _auRep = new AppUserRepository<AppUser>();
        }

        #region Login

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser ap = _auRep.FirstOrDefault(x => x.UserName == appUser.UserName && x.Password == appUser.Password);

            if (ap != null)
            {
                if (ap.IsVerified is true)
                {
                    if (ap.Role == ENTITIES.Enums.UserRole.Admin)
                    {
                        Session["Admin"] = ap;
                        return RedirectToAction("AdminCatalog", "Catalog");
                    }
                    return RedirectToAction("MemberCatalog", "Catalog");
                }
                ViewBag.Message = "Lütfen Mail'inize Gelen Linkten Kaydınızı Onaylayın";
            }
            ViewBag.Message = "Kullanıcı Bilgileri Geçersiz";
            return View();
        }

        #endregion

        #region Register

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AppUser appUser)
        {
            if (appUser.Password is null)
            {
                ViewBag.Message = "Lütfen Şifre Girin";
            }
            else
            {
                 AppUser ap = _auRep.FirstOrDefault(x => x.UserName == appUser.UserName && x.Email == appUser.Email);
                 if (ap == null)
                 {
                 _auRep.Add(appUser);
                 BuildEmailTemplate(appUser.ID);
                 return RedirectToAction("Login");
                 }
                 ViewBag.Message = "Bu Kullanıcı Adı veya Mail Sistemde Mevcut";
            }

            return View();

        }

        #endregion

        #region ConfirmRegistration

        public ActionResult Confirm(int regID)
        {
            ViewBag.regID = regID;

            IndexVM ivm = new IndexVM
            {
                AppUser = _auRep.Find(regID)
            };
            _auRep.UpdateMail(ivm.AppUser);
            return View(ivm);
        }

        #endregion

        #region E-Mail Sender

        private void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = _auRep.FirstOrDefault(x => x.ID == regID);
            var url = "https://localhost:44323/" + "Home/Confirm?regID=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your Account Is Successfully Created",body,regInfo.Email);
        }

        private static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "businessmail@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body= sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("businessmail@gmail.com", "mailpassword");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}