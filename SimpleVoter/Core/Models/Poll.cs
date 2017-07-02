using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public Poll()
        {
            Answers = new Collection<Answer>();
        }
    }
}