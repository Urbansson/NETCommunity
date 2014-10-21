﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetCommunity.Models;
using NetCommunity.ViewModels;

namespace NetCommunity.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Something to change
        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Reciver).Include(m => m.Sender);
            return View(messages.ToList());
        }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
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
            
            DisplayMessageViewModel MessageView = new DisplayMessageViewModel();
            MessageView.Sender = Message.Sender.UserName;
            MessageView.Title = Message.Title;
            MessageView.Content = Message.Content;
            MessageView.Time = Message.Time;

            return View(MessageView);
          
        }
    }
}
