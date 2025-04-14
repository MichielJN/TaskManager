using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Abstractions;
using SQLiteNetExtensions.Attributes;

namespace TaskManager.MVVM.Models
{
    public class SuccessorPredecessorTaskIds : TableData
    {
        [ForeignKey(typeof(ProjectTask))]
        public int PredecessorTaskId { get; set; }

        [ManyToOne]
        public ProjectTask PredecessorTask { get; set; }

        [ForeignKey(typeof(ProjectTask))]
        public int SuccessorTaskId { get; set; }

        [ManyToOne]
        public ProjectTask SuccessorTask { get; set; }
    }
}
