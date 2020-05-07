using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class SectionstoTopics
    {
        public int SectionstoTopicsID { get; set; }
        public int SectionsID { get; set; }
        public int TopicsID { get; set; }
        public virtual Sections Sections { get; set; }
        public virtual Topics Topics { get; set; }

    }
}