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
    public class SendController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Send
        public ActionResult Index()
        {

            var UserNames = db.Users.Select(x => new SelectListItem{
                                                            Text = x.UserName,
                                                            Value = x.Id
            });

            SendMessageViewModel model = new SendMessageViewModel();

            model.Users = new SelectList(UserNames, "Value", "Text");

            return View(model);
        }

        // POST: Send/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Title,Content,Reciver,SelectedUsers")] SendMessageViewModel message)
        {
            foreach (String x in message.SelectedUsers)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
   
            /*var Sender = db.Users.Find(User.Identity.GetUserId());

            var Reciver = (ApplicationUser)null;
            Reciver = db.Users.SingleOrDefault(u => u.UserName.Equals(message.Reciver));

            if (ModelState.IsValid && Reciver != null)
            {
                Message dbMessage = new Message();
                
                dbMessage.SenderId = Sender.Id;
                dbMessage.ReciverId = Reciver.Id;

                dbMessage.Title = message.Title;
                dbMessage.Content = message.Content;

                dbMessage.IsRead = false;
                dbMessage.Time = DateTime.Now;

                db.Messages.Add(dbMessage);

                Reciver.TotalMessages += 1;

                db.SaveChanges();

                //string output = String.Format("Message number {0} have been sent to {1},{2}", dbMessage.Id, dbMessage.Reciver.UserName, dbMessage.Time);

                return RedirectToAction("Index");
            }

            if (Reciver == null)
                ModelState.AddModelError("notFound", "Reciver was not found");

            return View(message);*/
            return RedirectToAction("Index");

        }

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
