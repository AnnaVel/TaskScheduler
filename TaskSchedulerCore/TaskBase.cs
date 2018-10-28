using System;

namespace TaskSchedulerCore
{
    public abstract class TaskBase : ITask
    {
        private readonly string name;

        private string description;

        public TaskBase(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.OnTaskChanged();
                }
            }
        }

        public abstract DateTime TimeDue { get; }

        public abstract TaskType TaskType { get; }

        public bool TaskIsOverdue
        {
            get
            {
                return this.TimeDue < DateTime.Now;
            }
        }

        protected virtual void OnTimeDueChanged()
        {
            if (this.TimeDueChanged != null)
            {
                this.TimeDueChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler TimeDueChanged;

        protected virtual void OnTaskChanged()
        {
            if (this.TaskChanged != null)
            {
                this.TaskChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler TaskChanged;
    }
}
