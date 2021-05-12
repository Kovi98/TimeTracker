using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Timetracker.Entity
{
    public partial class Job
    {
        public Job()
        {
            TaskTracks = new HashSet<TaskTrack>();
        }

        public int Id { get; set; }
        [Display(Name="Název")]
        [Required(ErrorMessage = "Je potřeba vyplnit název druhu práce")]
        [MaxLength(50, ErrorMessage = "Název může být dlouhý maximálně 50 znaků")]
        public string Name { get; set; }

        public virtual ICollection<TaskTrack> TaskTracks { get; set; }
    }
}
