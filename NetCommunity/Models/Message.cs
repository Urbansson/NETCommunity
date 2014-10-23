using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCommunity.Models
{
    /// <summary>
    /// A model for a message in our system.
    /// A message has a sender, a reciecer, a title, a content and a timestamp from when it is sent
    /// </summary>
    public class Message
    
    {


       [Key]
       public int Id { get; set; } // Id for Entity Framework and database

       [Required]
       public String SenderId { get; set; }

       public virtual ApplicationUser Sender { get; set; }

       [Required]
       public String ReciverId { get; set; }

       public virtual ApplicationUser Reciver { get; set; }
  
       [Required]
       public String Title { get; set; }
       public String Content { get; set; }
       [Required]
       public Boolean IsRead { get; set; }
       [Required]
       public DateTime Time { get; set; }

    }
}