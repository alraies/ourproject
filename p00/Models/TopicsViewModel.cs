using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class TopicsViewModel
    {
        
        public int Id { get; set; }
        
        public string TopicName { get; set; }
        public string Description { get; set; }
       
        
        public int TotalPoints { get; set; }
        
        public Boolean ReqDoc { get; set; }
        public int DocPoints { get; set; }
        public bool Checked { get; set; }
    }
}