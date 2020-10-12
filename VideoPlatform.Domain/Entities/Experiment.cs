using System;
using VideoPlatform.Domain.Enums;

namespace VideoPlatform.Domain.Entities
{
    public class Experiment : Entity<int>
    {
        public string Name { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual AppUser CreatedUser { get; set; }

        public ExperimentStatus Status { get; set; }

        public ExperimentType Type { get; set; }
    }
}