using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;

namespace TaskManager.MVVM.Models
{
    public class Stage : TableData
    {
        public string Description { get; set; } = "";
        [ManyToOne]
        public List<ProjectTask> ProjectTasks { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan timeSpent { get; set; } = TimeSpan.Zero;
        public TimeSpan PlannedHours { get; set; } = TimeSpan.Zero;
        [ForeignKey(typeof(Project))]
        public int ProjectId { get; set; }
        [ManyToOne]
        public Project Project { get; set; }
        [ForeignKey(typeof(Schedule))]
        public int ScheduleId { get; set; }
        [ManyToOne]
        public Schedule Schedule { get; set; }
        public Stage()
        {

        }
        public Stage(string name, Project project)
        {
            this.Name = name;
            this.Project = project;
        }

    }
}
