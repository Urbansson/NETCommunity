using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace NetCommunity.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; } // Id for Entity Framework and database
        public DateTime LoginTime { get; set; }
        // Navigation property
        public virtual ApplicationUser User { get; set; }

    }
}