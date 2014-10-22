using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NetCommunity.ViewModels
{
    /// <summary>
    /// A viewmodel to get data when we want to read a single message and its content
    /// </summary>
    public class DisplayMessageViewModel
    {

        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sender")]
        public string Sender { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Content { get; set; }
        
        [Display(Name = "Recieved Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Error")]
        public string ErrorMessage { get; set; }

    }
}