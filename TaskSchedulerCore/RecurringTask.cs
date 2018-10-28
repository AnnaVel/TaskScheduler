using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSchedulerCore
{
    public class RecurringTask : TaskBase
    {
        private TimeSpan interval;
        private DateTime lastCalculatedTimeDue;
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

        public TimeSpan Interval
        {
            get
            {
                return this.interval;
            }
            set
            {
                if(this.interval != value)
                {
                    this.interval = value;
                    this.RecalculateTimeDue();
                    this.OnTaskChanged();
                }
            }
        }

        public override DateTime TimeDue
        {
            get
            {
                return this.lastCalculatedTimeDue;
            }
        }

        public override TaskType TaskType
        {
            get
            {
                return TaskType.RecurringTask;
            }
        }

        public void AddOccurrence(DateTime moment, TimeSpan duration)
        {
            this.taskOccurrences.Add(new TaskOccurrence(moment, duration));
            this.RecalculateTimeDue();
            this.OnTaskChanged();
        }

        private DateTime GetLastTimeTaskWasPerformed()
        {
            TaskOccurrence lastOccurence = this.taskOccurrences.LastOrDefault();
            if(lastOccurence != null)
            {
                return lastOccurence.OccurrenceMoment;
            }
            else
            {
                return default(DateTime);
            }
        }

        private DateTime GetNextTimeTaskShouldBePerformed()
        {
            DateTime lastOccurrence = this.GetLastTimeTaskWasPerformed();
            if(lastOccurrence == default(DateTime))
            {
                lastOccurrence = DateTime.Now;
            }

            return lastOccurrence += this.Interval;
        }

        private void RecalculateTimeDue()
        {
            DateTime newTimeDue = this.GetNextTimeTaskShouldBePerformed();

            if (this.lastCalculatedTimeDue != newTimeDue)
            {
                this.lastCalculatedTimeDue = newTimeDue;
                this.OnTimeDueChanged();
                this.OnTaskChanged();
            }
        }
    }
}
