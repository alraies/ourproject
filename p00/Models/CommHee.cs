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
        [DisplayName("السنة الدراسية")]
        public string AcdYea { get; set; }
        [DisplayName("رئيس االلجنة")]
        public string head { get; set; }

        [DisplayName(" االلجنة")]
        public int CommitHeesid { get; set; }
        [DisplayName(" االلجنة")]
        public CommitHees CommitHees { get; set; }
        public ICollection<CommHeeMembers> CommHeeMembers { get; set; }
    }
}