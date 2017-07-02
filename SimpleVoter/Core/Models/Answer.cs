using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public Poll Poll { get; set; }
        public int PollId { get; set; }
    }
}