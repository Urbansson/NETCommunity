using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NetCommunity.ViewModels
{
    public class GroupDetailsViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Members")]
        public IEnumerable<String> Members { get; set; }

    }
}