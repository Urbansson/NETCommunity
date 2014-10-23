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

        public IEnumerable<MessageInfo> Messages { get; set; }


        [Display(Name = "Total Number of Messages")]
        public int TotalMessages { get; set; }

        [Display(Name = "Read Messages")]
        public int ReadMessages { get; set; }

        [Display(Name = "Deleted Messages")]
        public int DeletedMessages { get; set; }

    }

    public class MessageInfo
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