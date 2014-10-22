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
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime LoginTime { get; set; }
        public int Logins { get; set; }
        public int UnreadMessages { get; set; }
        public String UserName { get; set; }
    }
}