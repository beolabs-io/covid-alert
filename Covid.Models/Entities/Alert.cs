using System;
using System.ComponentModel.DataAnnotations;

namespace Covid.Models.Entities
{
    public class Alert
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public DateTime When { get; set; }

        public Alert()
        {
            this.When = DateTime.Now;
        }
    }
}
