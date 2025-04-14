using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.MVVM.Models
{
    public class Schedule : Project
    {
        [ForeignKey(typeof(User))]
        public int OwnerId { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public User Owner { get; set; }

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
            {   new Stage("Monday", this),
                new Stage("Tuesday", this), 
                new Stage("Wednesday", this), 
                new Stage("Thursday", this),
                new Stage("Friday", this),
                new Stage("Saturday", this),
                new Stage("Sunday", this)    };
            this.Stages = weekDays;
            this.PlannedHours = TimeSpan.FromHours(plannedHours);
            this.Owner = user;
            

            this.Id = App.ScheduleRepo.SaveEntityWithChildren(this);
            user.Schedule = this;
            user.ScheduleId = this.Id;
            App.UserRepo.SaveEntityWithChildren(user);
            
            string hallo = "";
        }


    }
}
