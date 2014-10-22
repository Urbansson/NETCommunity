using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunity.ViewModels
{
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
        [Display(Name = "Users")]
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<String> SelectedUsers { get; set; }

    }
}