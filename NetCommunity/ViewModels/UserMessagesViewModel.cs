using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NetCommunity.ViewModels
{
    /// <summary>
    /// Recieved messages from users and how many messages unread messages from each sender.
    /// </summary>
    public class UserMessagesViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sender")]
        public String Sender { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Unread Messages")]
        public int NumberOfMessages { get; set; }
    }
}