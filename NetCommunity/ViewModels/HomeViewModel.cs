using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace NetCommunity.ViewModels
{

    /// <summary>
    /// Viewmodel for the homepage
    /// </summary>
    public class HomeViewModel
    {
        [Display(Name = "Last login time")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime LoginTime { get; set; }
        
        [Display(Name = "Number of logins past 30 days")]
        public int Logins { get; set; }
        

        [Display(Name = "Number of unread messages")]
        public int UnreadMessages { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public String UserName { get; set; }
    }
}