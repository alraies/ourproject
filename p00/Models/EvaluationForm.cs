
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace p00.Models
{
    public class EvaluationForm
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "السنه")]
        public DateTime year { get; set; }
        [Display(Name = "الفعاله")]
        public bool iscurent { get; set; }
        public ICollection<EvaluaationFormtoSections> EvaluaationFormtoSections { get; set; }
      //  public ICollection<TopicEV> TopicEVs { get; set; }
       

    }
}