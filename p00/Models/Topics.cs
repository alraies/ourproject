using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class Topics
    {
        [Key]
        public int Pid { get; set; }
        [Required]
        [Display(Name="عنوان الفقرة")]
        public string TopicName { get; set; }
        [Required]
        [Display(Name = "الدرجة القصوى")]
        public int Points { get; set; }
        [Required]
        [Display(Name = "وصف الفقرة والملاحظات")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "تتطلب وثيقة ؟")]
        public Boolean ReqDoc { get; set; }
        [Required]
        [Display(Name = "وزن درجة الوثيقة")]
        public int PointForDoc { get; set; }
        [Required]
        [Display(Name = "المسؤول عن الموافقة والتقييم")]
        public string Approvingly { get; set; }
    }
}