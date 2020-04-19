using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class CommHeeMembers
    {
        public int Id { get; set; }

        public string Role { get; set; }

        public int CommHeeid { get; set; }
        public CommHee CommHee { get; set; }
        public int Teacherid { get; set; }
        [Display(Name = "اسم الاستاذ")]
        public Teacher Teacher { get; set; }

    }
}