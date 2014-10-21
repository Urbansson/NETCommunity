using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NetCommunity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // Navigation property
        public virtual ICollection<Login> NrOfLogins { get; set; }
        
        public virtual ICollection<Message> SentMessages { get; set; }

        public virtual ICollection<Message> RecivedMessages { get; set; }

        public ApplicationUser()
        {
            NrOfLogins = new List<Login>();
            //RecivedMessages = new List<Message>();
            SentMessages = new List<Message>();
            RecivedMessages = new List<Message>();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("NetCommunityContext", throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Login> NrOfLogins { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                        .HasRequired(x => x.Reciver)
                        .WithMany(x => x.RecivedMessages)
                        .HasForeignKey(x => x.ReciverId)
                        .WillCascadeOnDelete(false);
    
            modelBuilder.Entity<Message>()
                        .HasRequired(x => x.Sender)
                        .WithMany(x => x.SentMessages)
                        .HasForeignKey(x => x.SenderId)
                        .WillCascadeOnDelete(false);

        }
    }
}