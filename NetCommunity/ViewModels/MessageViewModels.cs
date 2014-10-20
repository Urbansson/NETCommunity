using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NetCommunity.ViewModels
{
    public class MessageViewModels
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public Boolean IsRead { get; set; }
        public DateTime SentTime { get; set; }

        public String Sender { get; set; }
        public String Reciver { get; set; }

    }

    public class SendMessageViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public String Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public String Content { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Reciver")]
        public String Reciver { get; set; }

    }

}