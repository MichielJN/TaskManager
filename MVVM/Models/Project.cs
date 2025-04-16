using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;
using static Microsoft.Maui.Controls.Device;

namespace TaskManager.MVVM.Models
{
    public class Project : TableData
    {
        public string Description { get; set; } = "";

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Stage> Stages { get; set; } = new List<Stage>();
        [ForeignKey(typeof(User))]
        public int? UserId { get; set; }
        [ManyToOne]
        public User? User { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public TimeSpan PlannedHours { get; set; } = TimeSpan.Zero;
        public TimeSpan SpentHours { get; set; } = TimeSpan.Zero;

        public Project()
        {

        }
        public Project(float plannedHours, List<Stage> stages)
        {
            this.PlannedHours = TimeSpan.FromDays(plannedHours);
            this.Stages = stages;
        }


        public void CalculateTotalTimeSpent()
        {
            this.SpentHours = TimeSpan.Zero;
            foreach(Stage stage in Stages)
            {
                foreach(ProjectTask projectTask in stage.ProjectTasks)
                {
                    foreach(StudySession session in projectTask.studySessions)
                    {
                        this.SpentHours += session.ElapsedTime;
                    }
                }
            }
        }

    }
}
