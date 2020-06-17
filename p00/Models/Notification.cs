using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class Notification
    {
        public int id { get; set; }
        public int RecipientID { get; set; }
        public int AccountontID { get; set; }
        public string Messagee { get; set; }
        public DateTime AddedOn { get; set; }
        public bool issee { get; set; }
    }
}