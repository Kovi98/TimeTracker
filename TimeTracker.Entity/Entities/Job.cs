using System;
using System.Collections.Generic;

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
        public string Name { get; set; }

        public virtual ICollection<TaskTrack> TaskTracks { get; set; }
    }
}
