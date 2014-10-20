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

namespace NetCommunity.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //System.Data.Entity.Database.SetInitializer(new MyInitializer());          
            
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            //var data = db.NrOfLogins.Where(d => (d.LoginTime.AddDays(30) > DateTime.Now) && d.User.Id == currentUser.Id);
            var data = db.NrOfLogins.Where(d => d.User.Id == currentUser.Id);

            var ViewModel = new LoginsViewModel();

            DateTime Last30Days = DateTime.Now.AddDays(-30);

            ViewModel.Logins = data.Count(time => time.LoginTime >= Last30Days);
            try
            {
                ViewModel.LoginTime = data.OrderByDescending(x => x.LoginTime).Skip(1).Take(1).FirstOrDefault().LoginTime;
            }
            catch (NullReferenceException)
            {
                                ViewModel.LoginTime = DateTime.Now;

            }
            ViewModel.UserName = currentUser.UserName;



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