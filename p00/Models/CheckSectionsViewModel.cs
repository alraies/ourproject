using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class CheckSectionsViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "اسم المحور")]
        public string SectionName { get; set; }
        [Required]
        [Display(Name = "وصف المحور")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "وزن المحور")]
        public int TotalPoints { get; set; }
        public bool Checked { get; set; }
    }
}