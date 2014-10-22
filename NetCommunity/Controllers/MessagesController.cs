using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetCommunity.Models;
using NetCommunity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

///Controller for handling messages on site

namespace NetCommunity.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Something to change
        // GET: Messages
        public ActionResult Index()
        {

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            var UserMessages = db.Messages.Where(u => u.ReciverId == currentUser.Id).GroupBy(g => g.Sender).Select(m => new UserMessagesViewModel
            {
                Sender = m.Key.UserName,
                NumberOfMessages = m.Where(k => k.IsRead == false).Count()
            });

            return View(UserMessages);
        }

        /// <summary>
        /// Takes a username as argument and returns a view with the users inbox
        /// </summary>
        /// <param name="user">The registred email of the user</param>
        /// <returns>The a view of the inbox for the user</returns>
        public ActionResult UserMessages(String user)
        {
            System.Diagnostics.Debug.WriteLine("user");

            var currentUser = db.Users.Find(User.Identity.GetUserId());
            System.Diagnostics.Debug.WriteLine(currentUser.UserName);

            var sendingUser = db.Users.Where(u => u.UserName.Equals(user)).FirstOrDefault();

            System.Diagnostics.Debug.WriteLine(sendingUser.UserName);

            if (sendingUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Messages = db.Messages.Where(u => u.ReciverId == currentUser.Id && u.SenderId == sendingUser.Id).Select(m => new ShowUserMessagesViewModel
            { 
                Id = m.Id,
                IsRead = m.IsRead,
                Sender = m.Sender.UserName,
                Title = m.Title,
                Time = m.Time
            });

            if (Messages == null)
            {
                return HttpNotFound();
            }

            return View(Messages);
        }

        /// <summary>
        /// To read a specified message
        /// </summary>
        /// <param name="id">The id of the message we want to read</param>
        /// <returns>Returns a view showing the message</returns>
        public ActionResult Read(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message Message = db.Messages.Find(id);

            if (Message == null)
            {
                return HttpNotFound();
            }

            Message.IsRead = true;
            db.SaveChanges();

            DisplayMessageViewModel MessageView = new DisplayMessageViewModel();
            MessageView.Sender = Message.Sender.UserName;
            MessageView.Title = Message.Title;
            MessageView.Content = Message.Content;
            MessageView.Time = Message.Time;



            return View(MessageView);

        }

        /*
        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.ReciverId = new SelectList(db.Users, "Id", "Email");
            ViewBag.SenderId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SenderId,ReciverId,Title,Content,IsRead,Time")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReciverId = new SelectList(db.Users, "Id", "Email", message.ReciverId);

            ViewBag.SenderId = new SelectList(db.Users, "Id", "Email", message.SenderId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReciverId = new SelectList(db.Users, "Id", "Email", message.ReciverId);
            ViewBag.SenderId = new SelectList(db.Users, "Id", "Email", message.SenderId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SenderId,ReciverId,Title,Content,IsRead,Time")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReciverId = new SelectList(db.Users, "Id", "Email", message.ReciverId);
            ViewBag.SenderId = new SelectList(db.Users, "Id", "Email", message.SenderId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */

        /// <summary>
        /// Method the clear the database connection
        /// </summary>
        /// <param name="disposing"></param>
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
