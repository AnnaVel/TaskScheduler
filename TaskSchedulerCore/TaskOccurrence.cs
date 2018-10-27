using System;
using System.Collections.Generic;
using System.Text;

namespace TaskSchedulerCore
{
    public class TaskOccurrence : IComparable
    {
        public TaskOccurrence()
        {
        }

        public TaskOccurrence(DateTime moment, TimeSpan duration)
        {
            this.OccurrenceMoment = moment;
            this.OccurrenceDuration = duration;
        }

        public DateTime OccurrenceMoment { get; }
        public TimeSpan OccurrenceDuration { get; }

        public int CompareTo(object obj)
        {
            TaskOccurrence otherOccurrence = obj as TaskOccurrence;

            if(otherOccurrence == null)
            {
                throw new InvalidOperationException("Cannot compare to something that is not of the same type");
            }

            return this.OccurrenceMoment.CompareTo(otherOccurrence.OccurrenceMoment);
        }
    }
}
