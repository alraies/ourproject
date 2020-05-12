using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class TopicEV
    {
        public int Id { get; set; }
        public int EvaluationFormId { get; set; }
        public EvaluationForm EvaluationForm { get; set; }
        public int SectionId { get; set; }
        public Sections Sections { get; set; }
        public  int TopicId { get; set; }
        public Topics Topics { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int Points { get; set; }
        public bool Approved { get; set; }
        public virtual  Document Document { get; set; }

    }
}