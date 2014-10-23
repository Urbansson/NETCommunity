using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NetCommunity.ViewModels
{

    /// <summary>
    /// A viewmodel to used to show all messages in inbox. Does not show content of message.
    /// </summary>
    public class ShowUserMessagesViewModel
    {

        [Required]
        [Display(Name = "Message Id")]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sender")]
        public String Sender { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public String Title { get; set; }

        [Required]
        [Display(Name = "Is Read")]
        public bool IsRead { get; set; }

        [Display(Name = "Recieved Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Display(Name = "Total Number of Messages")]
        public int TotalMessages { get; set; }

        [Display(Name = "Read Messages")]
        public int ReadMessages { get; set; }

        [Display(Name = "Deleted Messages")]
        public int DeletedMessages { get; set; }

    }




}