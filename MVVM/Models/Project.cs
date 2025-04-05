using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;

namespace TaskManager.MVVM.Models
{
    public class Project : TableData
    {
        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
        [ManyToOne]
        public User User { get; set; }
        [OneToMany]
        public List<Task> Tasks { get; set; }
    }
}
