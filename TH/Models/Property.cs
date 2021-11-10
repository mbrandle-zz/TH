using System;
using System.ComponentModel.DataAnnotations;

namespace TH.Models
{
    public class Property
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string title {get;set;}

        [Required]
        public string address { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public DateTime disabled_at { get; set; }

        [Required]
        [MaxLength(35)]
        public string status { get; set; }
    }
}
