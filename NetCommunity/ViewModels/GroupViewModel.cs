using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NetCommunity.ViewModels
{
    public class GroupViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Number of Members")]
        public int NrOfMembers { get; set; }

        [Display(Name = "Member")]
        public Boolean Member { get; set; }

    }
}