using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetCommunity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunity.ViewModels;


/// Controller to for passing user to relevant page on site
/// 
namespace NetCommunity.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            var ViewModel = new HomeViewModel();
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            var Logins = db.NrOfLogins.Where(d => d.User.Id == currentUser.Id);
            DateTime Last30Days = DateTime.Now.AddDays(-30);
            ViewModel.Logins = Logins.Count(time => time.LoginTime >= Last30Days);

            int UnreadMessages = db.Messages.Where(u => u.ReciverId == currentUser.Id && u.IsRead == false).Count();

            try
            {
                ViewModel.LoginTime = Logins.OrderByDescending(x => x.LoginTime).Skip(1).Take(1).FirstOrDefault().LoginTime;
            }
            catch (NullReferenceException)
            {
                                ViewModel.LoginTime = DateTime.Now;

            }

            ViewModel.UserName = currentUser.UserName;
            ViewModel.UnreadMessages = UnreadMessages;


            System.Diagnostics.Debug.WriteLine(ViewModel.Logins);
            System.Diagnostics.Debug.WriteLine( ViewModel.LoginTime);
            System.Diagnostics.Debug.WriteLine( ViewModel.UserName);

            
            return View(ViewModel);
        }

        /*
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
         * */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}