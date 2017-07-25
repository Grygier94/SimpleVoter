using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.ViewModels
{
    public class PollListViewModel
    {
        [Display(Name = "Number")]
        public int PollId { get; set; }

        public string Question { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }
    }
}