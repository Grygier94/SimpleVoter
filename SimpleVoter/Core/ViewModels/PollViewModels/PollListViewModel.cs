using System;
using System.ComponentModel.DataAnnotations;
using SimpleVoter.Core.Enums;

namespace SimpleVoter.Core.ViewModels.PollViewModels
{
    public class PollListViewModel
    {
        [Display(Name = "Number")]
        public int PollId { get; set; }

        public string Question { get; set; }

        public Visibility Visibility { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}