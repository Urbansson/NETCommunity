using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunity.ViewModels
{
    /// <summary>
    /// Viewmodel for sending messages
    /// </summary>
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
        public IEnumerable<SelectListItem> Users { get; set; }

        //Help parameters to render view

        public String SuccessMessage { get; set; }

        public SendMessageViewModel()
        {
            SuccessMessage = null;
        }

    }
}