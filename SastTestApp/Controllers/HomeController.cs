using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using SastTestApp.Attributes;
using SastTestApp.Models;

namespace SastTestApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                ViewBag.CapMsg = "Captcha Is Not Valid";
                return View();
            }
            //if (user.UserName == "jagrti" && user.Password == "abc")
            //{
            //    HttpContext.Session["loggedInUser"] = "jagrti";
            //    user.Email = "prajapatjagrti023@gmail.com";
            //    SetLoggedInUser(user);
            //    return RedirectToAction("ShowUserDetails");
            //}
            DataProvider dataProvider = new DataProvider();
            if(dataProvider.IsUserExist(user))
            {
                HttpContext.Session["loggedInUser"] = "jagrti";
                SetLoggedInUser(user);
                return RedirectToAction("ShowUserDetails");
            }
            ViewBag.InvalidCred = "User Credentials are Incorrect!";
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            DataProvider dataProvider = new DataProvider();
            user.Password = RandomString(6);
            dataProvider.saveUser(user);
            string msg = "Your password to log into sast app is " + user.Password;
            sendEmail(user.Email,msg);
            return RedirectToAction("Login");
        }

        [CustomAuthorization]
        public ActionResult ShowUserDetails()
        {
            User user = GetLoggedInUser();
            return View(user);
        }
        public User GetLoggedInUser()
        {
            return (User)HttpContext.Session["User"];
        }
        public void SetLoggedInUser(User user)
        {
            DataProvider dataProvider = new DataProvider();
            HttpContext.Session["User"] = dataProvider.getUserDetails(user);
        }
        public void sendEmail(string receiver,string msg)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("prajapatjagrti023@gmail.com");
                message.To.Add(new MailAddress(receiver));
                message.Subject = "welcome to sast app";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = msg;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("prajapatjagrti023@gmail.com", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}