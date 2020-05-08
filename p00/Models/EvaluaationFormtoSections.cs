using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class EvaluaationFormtoSections
    {
        public int id { get; set; }
        public int EvaluationFormID { get; set; }
        public int SectionsID { get; set; }
        public virtual EvaluationForm EvaluationForm { get; set; }
        public virtual Sections Sections { get; set; }
    }
}