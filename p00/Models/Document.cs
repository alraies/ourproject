using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class Document
    {
        [ForeignKey("TopicEV")]
        public int Id { get; set; }
        public bool Approved { get; set; }
        public virtual TopicEV TopicEV { get; set; }

    }
}