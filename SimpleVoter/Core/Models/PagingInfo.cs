using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Models
{
    public class PagingInfo
    {
        public int ItemsPerPage { get; set; }
        public int AllItems { get; set; }
        public int CurrentPage { get; set; }
        public int AllPages { get; set; }
    }
}