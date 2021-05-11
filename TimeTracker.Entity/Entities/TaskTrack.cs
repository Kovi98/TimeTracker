using System;
using System.Collections.Generic;

#nullable disable

namespace Timetracker.Entity
{
    public partial class TaskTrack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? JobId { get; set; }

        public virtual Job Job { get; set; }
    }
}
