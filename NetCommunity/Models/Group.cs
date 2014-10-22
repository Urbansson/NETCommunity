using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCommunity.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; } // Id for Entity Framework and database
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public Group()
        {
            ApplicationUsers = new List<ApplicationUser>();
        }
    }
}