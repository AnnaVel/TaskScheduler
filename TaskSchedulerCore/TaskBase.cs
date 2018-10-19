using System;
using System.Collections.Generic;
using System.Text;

namespace TaskSchedulerCore
{
    public abstract class TaskBase
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
                }
            }
        }
    }
}
