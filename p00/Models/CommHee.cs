using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class CommHee
    {
        public int id { get; set; }

        public string AcdYea { get; set; }
        [DisplayName("رئيس االلجنة")]
        public string head { get; set; }

        [DisplayName(" االلجنة")]
        public int CommitHeesid { get; set; }

        public CommitHees CommitHees { get; set; }
        public ICollection<CommHee> CommHeeCommHeeMembers { get; set; }
    }
}