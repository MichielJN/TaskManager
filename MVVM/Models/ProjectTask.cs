using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;

namespace TaskManager.MVVM.Models
{
    public class ProjectTask : TableData
    {
        public string? Description { get; set; }
        public int Status { get; set; }
        public int? Importance { get; set; }
        public int? Feasability { get; set; }
        public TimeSpan? PlannedHours { get; set; }
        public TimeSpan TimeSpent { get; set; } = TimeSpan.Zero;
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        [ForeignKey(typeof(TaskType))]
        public int TaskTypeId { get; set; }
        [ManyToOne]
        public TaskType TaskType { get; set; }
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
        [ManyToOne]
        public User User { get; set; }
        [ForeignKey(typeof(ProjectTask))]
        public int ParentTaskId { get; set; }
        [ManyToOne]
        public ProjectTask ParentTask { get; set; }
        [OneToMany]
        public List<ProjectTask> SubTasks { get; set; }
        [ManyToMany(typeof(SuccessorPredecessorTaskIds), "PredecessorTaskId", "SuccessorTaskId")]
        public List<ProjectTask> PredecessorTasks { get; set; }
        [ManyToMany(typeof(SuccessorPredecessorTaskIds), "SuccessorTaskId", "PredecessorTaskId")]
        public List<ProjectTask> SuccessorTasks { get; set; }
        [ForeignKey(typeof(Stage))]
        public int StageId { get; set; }
        [ManyToOne]
        public Stage Stage { get; set; }

        public DateTime? StudySessionStartTime { get; set; }
        [OneToMany]
        public List<StudySession> studySessions { get; set; } = new List<StudySession>();
        
        public ProjectTask()
        {

        }

        public ProjectTask(string name, int userId, User user, int status)
        {
            this.Name = name;
            this.UserId = userId;
            this.User = user;
            this.Status = status;
        }
        public void CalculateTotalTimeSpent()
        {
            foreach (StudySession session in this.studySessions)
            {
                this.TimeSpent += session.ElapsedTime;
            }
        }

    }
}
