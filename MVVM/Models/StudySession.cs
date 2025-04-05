using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;

namespace TaskManager.MVVM.Models
{
    public class StudySession : TableData
    {
        public TimeSpan ElapsedTime { get; set; }
        [ForeignKey(typeof(ProjectTask))]
        public int ProjectTaskId { get; set; }

        [ManyToOne]
        public ProjectTask ProjectTask { get; set; }

        public StudySession()
        {

        }

        public StudySession(TimeSpan elapsedTime, int ProjectTaskId, ProjectTask projectTask) 
        {
            this.ElapsedTime = elapsedTime;
            this.ProjectTaskId = ProjectTaskId;
            this.ProjectTask = projectTask;
        }

    }
}
