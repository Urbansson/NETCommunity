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

            SendMessageViewModel model = model = new SendMessageViewModel();

            if (TempData["SendSucces"] != null)
            {
                model.SendSuccess = "Message was sent";
            }
            else
            {
                model.SendSuccess = null;
            }

            var currenUser = db.Users.Find(User.Identity.GetUserId());


            var UserNames = db.Users.Select(x => new SelectListItem
            {
                Text = x.UserName,
                Value = x.Id
            });

            var GroupNames = db.Groups.Where(u => u.ApplicationUsers.Any(k => k.Id == currenUser.Id)).Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = "" + g.Id
            });


            model.Users = new SelectList(UserNames, "Value", "Text");

            model.Groups = new SelectList(GroupNames, "Value", "Text");
            return View(model);
        }

        public ActionResult Success()
        {

            var currenUser = db.Users.Find(User.Identity.GetUserId());

            Message LatestMessage = null;
            LatestMessage = db.Messages.Where(u => u.SenderId == currenUser.Id).OrderByDescending(t => t.Time).FirstOrDefault();

            if (LatestMessage == null || LatestMessage.Time.AddMinutes(1) < DateTime.Now)
                return RedirectToAction("Index");

            SendSuccessViewModel message = new SendSuccessViewModel();
            message.Message = String.Format("Message number {0} sent to {1}, {2}", LatestMessage.Id, LatestMessage.Reciver.UserName, LatestMessage.Time);
            return View(message);
        }


        // POST: Send/Create
        /// <summary>
        /// Sends a message to another user
        /// </summary>
        /// <param name="message">A viewmodel created from view containing sender, reciever, title of message, message, and a timestamp</param>
        /// <returns>A view back to index if everything is ok, else a view back to message send</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Title,Content,Reciver,SelectedUsers,SelectedGroups")] SendMessageViewModel message)
        {

            IEnumerable<String> AllRecivers = new List<String>();

            if (message.SelectedGroups != null && message.SelectedGroups.Any())
            {
                foreach (String groupId in message.SelectedGroups)
                {
                    int x = 0;
                    if (Int32.TryParse(groupId, out x))
                    {
                        var GroupUsers = db.Groups.Find(x).ApplicationUsers.Select(id => id.Id).ToList();
                        AllRecivers = AllRecivers.Union(GroupUsers);
                    }
                }
            }
            if (message.SelectedUsers != null && message.SelectedUsers.Any())
            AllRecivers = AllRecivers.Union(message.SelectedUsers);

            if (AllRecivers.Any())
            {
                foreach (String userId in AllRecivers)
                {
                    System.Diagnostics.Debug.WriteLine(userId);
                }

                if (ModelState.IsValid)
                {
                    Message dbMessage = new Message();

                    var Sender = db.Users.Find(User.Identity.GetUserId());

                    dbMessage.SenderId = Sender.Id;
                    dbMessage.Title = message.Title;
                    dbMessage.Content = message.Content;

                    dbMessage.IsRead = false;
                    dbMessage.Time = DateTime.Now;

                    var Reciver = (ApplicationUser)null;
                    foreach (String userId in AllRecivers)
                    {
                        Reciver = db.Users.Find(userId);
                        dbMessage.ReciverId = Reciver.Id;
                        db.Messages.Add(dbMessage);
                        db.SaveChanges();
                    }
                    TempData["SendSucces"] = "Somestuff";

                    return RedirectToAction("Index");
                }
            }
            /*
            if (message.SelectedUsers != null && message.SelectedGroups != null)
            {
                if (message.SelectedUsers.Any() || message.SelectedGroups.Any())
                {
                    if (ModelState.IsValid)
                    {
                        Message dbMessage = new Message();

                        var Sender = db.Users.Find(User.Identity.GetUserId());

                        dbMessage.SenderId = Sender.Id;
                        dbMessage.Title = message.Title;
                        dbMessage.Content = message.Content;

                        dbMessage.IsRead = false;
                        dbMessage.Time = DateTime.Now;


                        var Reciver = (ApplicationUser)null;
                        if (message.SelectedUsers.Any())
                        {
                            foreach (String user in message.SelectedUsers)
                            {
                                Reciver = db.Users.Find(user);
                                dbMessage.ReciverId = Reciver.Id;
                                db.Messages.Add(dbMessage);
                            }
                        }
                        db.SaveChanges();

                        TempData["SendSucces"] = "Somestuff";

                        return RedirectToAction("Index");
                    }
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            System.Diagnostics.Debug.WriteLine(error);

                        }
                    }
                }
            }






            /*
            foreach (String x in message.SelectedUsers)
            {
                System.Diagnostics.Debug.WriteLine(x);
            }
   
            var Sender = db.Users.Find(User.Identity.GetUserId());

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
            //return RedirectToAction("Index");

            //Should be a better way to solve this, is needed to keep the modelstate intace to show errors
            ModelState.AddModelError("SelectedGroups", "No reciver selected");
            ModelState.AddModelError("SelectedUsers", "No reciver selected");

            var currenUser = db.Users.Find(User.Identity.GetUserId());
            var UserNames = db.Users.Select(x => new SelectListItem
            {
                Text = x.UserName,
                Value = x.Id
            });
            var GroupNames = db.Groups.Where(u => u.ApplicationUsers.Any(k => k.Id == currenUser.Id)).Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = "" + g.Id
            });

            message.Users = new SelectList(UserNames, "Value", "Text");
            message.Groups = new SelectList(GroupNames, "Value", "Text");


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
