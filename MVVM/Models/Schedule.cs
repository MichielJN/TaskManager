using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;

namespace TaskManager.MVVM.Models
{
    public class Schedule : Project
    {
        [ForeignKey(typeof(User))]
        public int OwnerId { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public User Owner { get; set; }
        [ManyToOne]
        public List<Stage> Days { get; set; } = new List<Stage>();
        public Schedule() 
        { 

        }
        public Schedule(User owner, float plannedHours, List<Stage> stages)
        {
            this.Owner = owner;
            this.OwnerId = owner.Id;
            this.Stages = stages;
            this.PlannedHours = TimeSpan.FromDays(plannedHours);
        }

        public void CreateWeek(User user, float plannedHours)
        {
            List<Stage> weekDays = new List<Stage>()
            {   new Stage("Monday", this, 1),
                new Stage("Tuesday", this, 1), 
                new Stage("Wednesday", this, 1), 
                new Stage("Thursday", this, 1),
                new Stage("Friday", this, 1),
                new Stage("Saturday", this, 1),
                new Stage("Sunday", this, 1)    };
            this.Days = weekDays;
            this.PlannedHours = TimeSpan.FromHours(plannedHours);
            this.Owner = user;
            this.OwnerId = user.Id;
            


            

        }


    }
}
