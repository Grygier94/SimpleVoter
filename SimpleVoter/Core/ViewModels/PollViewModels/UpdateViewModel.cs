using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleVoter.Core.Enums;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Validators;

namespace SimpleVoter.Core.ViewModels.PollViewModels
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Display(Name = "Allow Multiple Answers")]
        public bool AllowMultipleAnswers { get; set; }

        public Visibility Visibility { get; set; }

        public ChartType ChartType { get; set; }

        [EnsureMinimumElements(2, "Question must contains at least 2 different answers!")]
        public List<Answer> Answers { get; set; }

        [CurrentDate]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        public UpdateViewModel()
        {
            Answers = new List<Answer>();
        }
    }
}