using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCommunity.Models
{
    public class Message
    
    {


       [Key]
       public int Id { get; set; } // Id for Entity Framework and database

       public String SenderId { get; set; }

       public virtual ApplicationUser Sender { get; set; }

       public String ReciverId { get; set; }

       public virtual ApplicationUser Reciver { get; set; }
  
       [Required]
       public String Title { get; set; }

       [Required]
       public String Content { get; set; }
       [Required]
       public Boolean IsRead { get; set; }
       [Required]
       public DateTime Time { get; set; }

    }
}