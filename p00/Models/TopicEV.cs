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
        public virtual EvaluationForm EvaluationForm { get; set; }
        public int SectionsId { get; set; }
        public virtual Sections Sections { get; set; }
        public  int TopicsId { get; set; }
        public virtual Topics Topics { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int Points { get; set; }
        public bool Approved { get; set; }
        public string Nameproved{ get; set; }
        //public virtual Document Document { get; set; }
        public virtual ICollection<Document> Document { get; set; }

    }
}