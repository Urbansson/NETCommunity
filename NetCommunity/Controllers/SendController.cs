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

/// Controller for sending messages between users

namespace NetCommunity.Controllers
{
    [Authorize]
    public class SendController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Send
        public ActionResult Index()
        {
            var UserList = new List<string>();


            var UserNames = db.Users.Select(x => new SelectListItem{
                                                            Text = x.UserName,
                                                            Value = x.UserName
            });


            SendMessageViewModel model = new SendMessageViewModel();

            model.Users = new SelectList(UserNames, "Value", "Text");

            return View(model);
        }

        
        
        // POST: Send/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Sends a message to another user
        /// </summary>
        /// <param name="message">A viewmodel created from view containing sender, reciever, title of message, message, and a timestamp</param>
        /// <returns>A view back to index if everything is ok, else a view back to message send</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Title,Content,Reciver")] SendMessageViewModel message)
        {
            var Sender = db.Users.Find(User.Identity.GetUserId());

            //var Reciver = db.Users.Where(u => u.UserName.Equals(message.Reciver)).First();

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
                db.SaveChanges();

                string output = String.Format("Message number {0} have been sent to {1},{2}", dbMessage.Id, dbMessage.Reciver, dbMessage.Time);

                return RedirectToAction("Index");
            }

            if (Reciver == null)
                ModelState.AddModelError("notFound", "Reciver was not found");
            return View(message);
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
