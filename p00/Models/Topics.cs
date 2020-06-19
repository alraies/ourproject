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
        public int Id { get; set; }
        [Required]
        [Display(Name ="أسم الفقرة")]
        public string TopicName { get; set; }
        [Required]
        [Display(Name = "وصف الفقرة")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "وزن الفقرة")]
        public int TotalPoints { get; set; }
        [Required]
        [Display(Name = "تتطلب وثائق؟!")]
        public Boolean ReqDoc { get; set; }
        [Required]
        [Display(Name = "وزن الوثيقة")]
        public int DocPoints { get; set; }
        [Required]
        [Display(Name = "اللجنه")]
        public int CommitHeesID { get; set; }
        public CommitHees CommitHees { get; set; }
        public ICollection<SectionstoTopics> SectionstoTopics { get; set; }
        public virtual ICollection<TopicEV> TopicEVs { get; set; }

    }
}