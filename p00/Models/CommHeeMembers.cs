using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class CommHeeMembers
    {
        public int Id { get; set; }

        public string Role { get; set; }

        public int CommHeeid { get; set; }
        public CommHee CommHee { get; set; }

    }
}