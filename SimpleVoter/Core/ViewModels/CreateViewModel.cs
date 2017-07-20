using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleVoter.Core.Models;

namespace SimpleVoter.Core.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Question { get; set; }

        [Display(Name = "Allow Multiple Answers")]
        public bool AllowMultipleAnswers { get; set; }

        public string UserId { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public CreateViewModel()
        {
            Answers = new Collection<Answer>();
        }
    }
}