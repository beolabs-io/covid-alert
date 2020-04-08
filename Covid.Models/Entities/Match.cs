using System;
using System.ComponentModel.DataAnnotations;

namespace Covid.Models.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime When { get; set; }

        [Required]
        public User UserX { get; set; }

        [Required]
        public User UserY { get; set; }

        public Match()
        {
            this.When = DateTime.Now;
        }
    }
}
