using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Models
{
    public class Activity
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int property_id { get; set; }

        [ForeignKey("property_id")]
        public virtual Property Property { get; set; }

        [Required]
        public DateTime schedule { get; set; }

        [Required]
        [MaxLength(255)]
        public string title { get; set; }

        [Required]
        public DateTime creted_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        [Required]
        [MaxLength(35)]
        public string status { get; set; }
    }
}
