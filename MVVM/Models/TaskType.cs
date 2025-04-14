using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;

namespace TaskManager.MVVM.Models
{
    public class TaskType : TableData
    {
        public string? Description { get; set; }
        [OneToMany]
        public List<ProjectTask> ProjectTasks { get; set; }
    }
}
