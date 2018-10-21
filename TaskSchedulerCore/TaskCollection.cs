using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TaskSchedulerCore
{
    public class TaskCollection : IEnumerable<ITask>
    {
        private List<ITask> allTasks;

        public TaskCollection()
        {
            this.allTasks = new List<ITask>();
        }

        public void AddTask(TaskBase task)
        {
            this.allTasks.Add(task);
            task.TimeDueChanged += Task_TimeDueChanged;
            this.OnCollectionChanged();
        }

        public bool RemoveTask(TaskBase task)
        {
            bool result = this.allTasks.Remove(task);
            task.TimeDueChanged -= Task_TimeDueChanged;
            this.OnCollectionChanged();

            return result;
        }

        private void OnCollectionChanged()
        {
            this.ReorderTasks();
        }

        private void Task_TimeDueChanged(object sender, EventArgs e)
        {
            this.ReorderTasks();
        }

        private void ReorderTasks()
        {
            this.allTasks.OrderBy(t => t.TimeDue);
        }

        public IEnumerator<ITask> GetEnumerator()
        {
            return this.allTasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.allTasks.GetEnumerator();
        }
    }
}
