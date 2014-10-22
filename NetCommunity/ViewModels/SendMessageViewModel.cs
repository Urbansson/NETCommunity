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

        [Display(Name = "Users")]
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<String> SelectedUsers { get; set; }

        [Display(Name = "Groups")]
        public IEnumerable<SelectListItem> Groups { get; set; }
        public IEnumerable<String> SelectedGroups { get; set; }

        [Display(Name = "Success")]
        public String SendSuccess { get; set; }

    }
}