using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models
{
    public class UniqueVisitor
    {
        public int Id { get; set; }
        public string IpAdress { get; set; }
        public ICollection<Poll> PollsParticipated { get; set; }

        public UniqueVisitor()
        {
            PollsParticipated = new Collection<Poll>();
        }
    }
}