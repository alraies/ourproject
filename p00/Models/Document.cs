using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace p00.Models
{
    public class Document
    {
        [Key]
        public int IdDocument { get; set; }
        public int TopicEVId { get; set; }
        public string Name { get; set; }
        public virtual TopicEV TopicEV { get; set; }
        //public virtual ICollection<TopicEV> TopicEVs { get; set; }

    }
}