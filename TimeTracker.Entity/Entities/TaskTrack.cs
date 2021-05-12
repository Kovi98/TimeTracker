using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Timetracker.Entity
{
    public partial class TaskTrack
    {
        public int Id { get; set; }
        [Display(Name = "Název")]
        [Required(ErrorMessage = "Je potřeba vyplnit název úkolu")]
        [MaxLength(200, ErrorMessage = "Název může být dlouhý maximálně 200 znaků")]
        public string Name { get; set; }
        [Display(Name = "Začátek")]
        [Required(ErrorMessage = "Je potřeba vyplnit začátek časování úkolu")]
        public DateTime? Start { get; set; }
        [Display(Name = "Konec")]
        [Required(ErrorMessage = "Je potřeba vyplnit konec časování úkolu")]
        public DateTime? End { get; set; }
        [Display(Name = "Druh práce")]
        [Required(ErrorMessage = "Je potřeba vyplnit druh práce")]
        public int? JobId { get; set; }

        public virtual Job Job { get; set; }

        //GETTERS
        [NotMapped]
        [Display(Name = "Délka")]
        public TimeSpan? Time => End - Start;
    }
}
