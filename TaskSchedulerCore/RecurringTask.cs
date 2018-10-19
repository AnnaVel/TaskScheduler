using System;
using System.Collections.Generic;
using System.Text;

namespace TaskSchedulerCore
{
    public class RecurringTask : TaskBase
    {
        private SortedSet<TaskOccurrence> taskOccurrences;

        public RecurringTask(string name)
            : base(name)
        {
            this.taskOccurrences = new SortedSet<TaskOccurrence>();
        }

        public IEnumerable<TaskOccurrence> TaskOccurrences
        {
            get
            {
                return this.taskOccurrences;
            }
        }

        public void AddOccurrence(DateTime moment, TimeSpan duration)
        {
            this.taskOccurrences.Add(new TaskOccurrence(moment, duration));
        }

        
    }
}
