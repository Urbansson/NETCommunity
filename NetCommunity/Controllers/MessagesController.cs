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

            UserMessagesViewModel model = new UserMessagesViewModel(); 

            var UserMessages = db.Messages.Where(u => u.ReciverId == currentUser.Id).GroupBy(g => g.Sender).Select(m => new UserMessageInfo
            {
                Sender = m.Key.UserName,
                NumberOfMessages = m.Where(k => k.IsRead == false).Count()
            });



            model.Messages = UserMessages;
            model.TotalMessages = currentUser.TotalMessages;
            model.ReadMessages = currentUser.ReadMessages;
            model.DeletedMessages = currentUser.RemovedMessages;



            return View(model);
        }

        /// <summary>
        /// Takes a username as argument and returns a view with the users inbox
        /// </summary>
        /// <param name="user">The registred email of the user, and a control argument to see if delete failed </param>
        /// <returns>The a view of the inbox for the user</returns>
        public ActionResult UserMessages(String user)
        {

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            var sendingUser = db.Users.Where(u => u.UserName.Equals(user)).FirstOrDefault();

            if (sendingUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Messages = db.Messages.Where(u => u.ReciverId == currentUser.Id && u.SenderId == sendingUser.Id).Select(m => new MessageInfo
            { 
                Id = m.Id,
                IsRead = m.IsRead,
                Title = m.Title,
                Time = m.Time
            });

            if (Messages == null)
            {
                return HttpNotFound();
            }

            ShowUserMessagesViewModel model = new ShowUserMessagesViewModel();
            model.Sender = sendingUser.UserName;
            model.Messages = Messages;
            model.TotalMessages = currentUser.TotalMessages;
            model.ReadMessages = currentUser.ReadMessages;
            model.DeletedMessages = currentUser.RemovedMessages;

            return View(model);
        }


        /// <summary>
        /// Reads a single message and returns a viewmodel of it 
        /// </summary>
        /// <param name="id">Id of the message to read</param>
        /// <param name="saveChangesError">Control argument to see if delete failed, default: false</param>
        /// <returns>Returns a viewmodel of relevant data</returns>

        public ActionResult Read(int? id, bool? saveChangesError = false)
        {
            var Reciver = db.Users.Find(User.Identity.GetUserId());


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message Message = db.Messages.Find(id);

            if (Message == null)
            {
                return HttpNotFound();
            }

            if(Message.Reciver != Reciver)
            {
                return RedirectToAction("Index");
            }
            if (Message.IsRead == false)
            {
                Reciver.ReadMessages += 1;
                Message.IsRead = true;
                db.SaveChanges();

            }
            DisplayMessageViewModel MessageView = new DisplayMessageViewModel();
            MessageView.Sender = Message.Sender.UserName;
            MessageView.Title = Message.Title;
            MessageView.Content = Message.Content;
            MessageView.Time = Message.Time;
            MessageView.Id = Message.Id;

            if (saveChangesError.GetValueOrDefault())
            {
                MessageView.ErrorMessage = "Delete failed.";
            }

            return View(MessageView);

        }


        /// <summary>
        /// Deletes a message
        /// </summary>
        /// <param name="id">The message to delete</param>
        /// <returns>A view for index page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (db.Messages.Find(id).Reciver == currentUser) { 

                try
                {
                    currentUser.RemovedMessages += 1;
                    Message message = db.Messages.Find(id);
                    db.Messages.Remove(message);
                    db.SaveChanges();
                }
                catch (DataException)
                {
                    //fix tempdata
                    return RedirectToAction("Read", new { id = id, saveChangesError = true });
                }
            }
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
