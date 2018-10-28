using System;
using System.Collections.Generic;
using System.Text;
using TaskSchedulerCore;
using TaskSchedulerCore.Utilities;

namespace TaskSchedulerDataBase
{
    internal class TaskDataBaseItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TaskType TaskType { get; set; }

        public DateTime TimeDue { get; set; }

        public TimeSpan Interval { get; set; }

        public TaskOccurrence[] TaskOccurences { get; set; }

        public override bool Equals(object obj)
        {
            TaskDataBaseItem other = obj as TaskDataBaseItem;

            if (obj == null)
            {
                return false;
            }

            return this.Name == other.Name &&
                this.Description == other.Description &&
                this.TaskType == other.TaskType &&
                this.TimeDue == other.TimeDue &&
                this.Interval == other.Interval &&
                HelperMethods.CollectionEqualsCollection(this.TaskOccurences, other.TaskOccurences);
        }

    }
}
