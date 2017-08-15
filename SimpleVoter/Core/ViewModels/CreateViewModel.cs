using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Enums;
using SimpleVoter.Core.Extensions;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Validators;

namespace SimpleVoter.Core.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Question { get; set; }

        [Display(Name = "Allow Multiple Answers")]
        public bool AllowMultipleAnswers { get; set; }

        public Visibility Visibility { get; set; }

        [CurrentDate]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [EnsureMinimumElements(2, "Question must contains at least 2 different answers!")]
        public ICollection<Answer> Answers { get; set; }

        public CreateViewModel()
        {
            Answers = new Collection<Answer>();
        }
    }
}