using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Models
{
    public class Survey
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int activity_id { get; set; }

        [ForeignKey("activity_id")]
        public virtual Activity Activity { get; set; }

        [Required]
        [Column(TypeName ="jsonb")]
        public string answers { get; set; }

        [Required]
        public DateTime creted_at { get; set; }
    }
}
