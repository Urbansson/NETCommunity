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
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
       /// <summary>
        /// Gets all groups from the database and formats them into a GroupViewModel
       /// </summary>
        /// <returns>GroupViewModel</returns>
        public ActionResult Index()
        {

           var currentUser = db.Users.Find(User.Identity.GetUserId());

            var groups = db.Groups.Select(g => new GroupViewModel
            {
                Name = g.Name,
                Id = g.Id,
                Description = g.Description,
                NrOfMembers = g.ApplicationUsers.Count(),
                Member = g.ApplicationUsers.Any(u => u.Id.Equals(currentUser.Id))
            });

            return View(groups.ToList());
        }

        // GET: Groups/Details/5
       /// <summary>
       /// Shows the details of a group, name, description and members
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            GroupDetailsViewModel model = new GroupDetailsViewModel();
            model.Name = group.Name;
            model.Description = group.Description;
            model.Members = group.ApplicationUsers.Select(name => (name.UserName)).ToList();

            return View(model);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
       /// <summary>
       /// Post method to create a Group
       /// </summary>
       /// <param name="group"></param>
       /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] GroupViewModel group)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                Group tmp = new Group();
                tmp.Name = group.Name;
                tmp.Description = group.Description;
                tmp.ApplicationUsers.Add(currentUser);

                db.Groups.Add(tmp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

       /// <summary>
       /// Post method to Join a group
       /// </summary>
       /// <param name="group"></param>
       /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join([Bind(Include = "Id")] GroupViewModel group)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                Group dbgroup = db.Groups.Find(group.Id);

                dbgroup.ApplicationUsers.Add(currentUser);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

       /// <summary>
       /// Post method to leave a group, if the person that leaves is the last one the group is deleted
       /// </summary>
       /// <param name="group"></param>
       /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave([Bind(Include = "Id")] GroupViewModel group)
        {
            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                Group dbgroup = db.Groups.Find(group.Id);
                dbgroup.ApplicationUsers.Remove(currentUser);

                if (!dbgroup.ApplicationUsers.Any())
                    db.Groups.Remove(dbgroup);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
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
