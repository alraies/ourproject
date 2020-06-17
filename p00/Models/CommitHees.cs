using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class CommitHees
    {
        public int id { get; set; }
        [DisplayName("اسم اللجنة")]
        public string comitname { get; set; }
        [DisplayName("وصف اللجنة")]
        public string comitDecipt { get; set; }
        [DisplayName("نوع اللجنة")]
        public string comitpriod { get; set; }
        [DisplayName("كيان ام لجنه")]
        public string comittype { get; set; }
        [DisplayName("تفعيل اللجنه")]
        public bool isActive { get; set; }

        public ICollection<Topics> Topics { get; set; }
        public ICollection<CommHee> CommHee { get; set; }
    }
}