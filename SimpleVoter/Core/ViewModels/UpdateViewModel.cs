using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Enums;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Validators;

namespace SimpleVoter.Core.ViewModels
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Display(Name = "Allow Multiple Answers")]
        public bool AllowMultipleAnswers { get; set; }

        public Visibility Visibility { get; set; }

        [EnsureMinimumElements(2, "Question must contains at least 2 different answers!")]
        public List<Answer> Answers { get; set; }

        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        public UpdateViewModel()
        {
            Answers = new List<Answer>();
        }
    }
}