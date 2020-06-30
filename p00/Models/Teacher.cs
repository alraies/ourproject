using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="الأسم الكامل")]
        public string FullName { get; set; }
        [Display(Name = "الجامعة")]
        public string University { get; set; }
        [Display(Name = "الكلية")]
        public string College { get; set; }
        [Display(Name = "القسم")]
        public string Department { get; set; }
        [Required]
        [Display(Name = "الشهادة الحاصل عليها")]
        public string Certificate { get; set; }
        [Display(Name = "تاريخ الحصول عليها")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime C_Date { get; set; }
        [Display(Name = "الجهة المانحة")]
        public string C_Doner { get; set; }
        [Display(Name = "التخصص العام")]
        public string GeneralSpecialization { get; set; }
        [Display(Name = "التخصص الدقيق")]
        public string Specialization { get; set; }
        [Display(Name = "اللقب العلمي")]
        public string ScientificTitle { get; set; }
        [Display(Name = "تاريخ الحصول  عليه")]
        public DateTime ST_Date { get; set; }
        [Display(Name = "الجهة المانحة")]
        public string ST_Doner { get; set; }
        [Required]
        [Display(Name = "الايميل")]
        public string Email { get; set; }
        [Display(Name = "رقم الهاتف")]
        public string Phone { get; set; }
        [Display(Name = "الاجازه")]
        public bool Vacation { get; set; }
        public ICollection<CommHeeMembers> CommHeeMembers { get; set; }
        public virtual  ICollection<TopicEV> TopicEVs { get; set; }
    }
}