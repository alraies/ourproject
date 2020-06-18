using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApplication2.Models;


namespace p00.Models
{
    public class UserToTeacher
    {
        //  public int id { get; set; }
        [Key, ForeignKey("User")]
        public string UserID { get; set; }
        public int TeacherID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Teacher Teacher { get; set; }

        
    }
}