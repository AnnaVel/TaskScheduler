using System;
using System.Collections.Generic;
using System.Text;

namespace TaskSchedulerCore
{
    public interface ITask
    {
        string Name { get; }

        string Description { get; }

        DateTime TimeDue { get; }

        bool TaskIsOverdue { get; }

        event EventHandler TimeDueChanged
    }
}
