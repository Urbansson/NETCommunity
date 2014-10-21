using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace NetCommunity.ViewModels
{
    public class LoginsViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime LoginTime { get; set; }
        public int Logins { get; set; }
        public String UserName { get; set; }




    }
}