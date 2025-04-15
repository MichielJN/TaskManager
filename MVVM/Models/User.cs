using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;

namespace TaskManager.MVVM.Models
{
    public class User : TableData
    {
        public string Password { get; set; }
        public string Salt { get; set; }
        [OneToMany]
        public List<Project> Projects { get; set; } = new List<Project>();
        [OneToMany]
        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
        [ForeignKey(typeof(Schedule))]
        public int? ScheduleId { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Schedule? Schedule { get; set; }


        public User()
        {

        }
        public User(string name, string password, string salt)
        {
            this.Name = name;
            this.Password = password;
            this.Salt = salt;
        }
    }

}
