using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerCore.Utilities;

namespace TaskSchedulerCore
{
    public class TaskCollection : IEnumerable<TaskBase>
    {
        private List<TaskBase> allTasks;

        public TaskCollection()
        {
            this.allTasks = new List<TaskBase>();
        }

        public void AddTask(TaskBase task)
        {
            this.allTasks.Add(task);
            task.TimeDueChanged += Task_TimeDueChanged;
            task.TaskChanged += Task_TaskChanged;
            this.ReorderTasks();
            this.OnCollectionChanged(this, new CollectionChangedEventArgs<TaskBase>()
            {
                ChangedElement = task,
                CollectionChangedType = CollectionChangedType.Add
            });
        }

        public bool RemoveTask(TaskBase task)
        {
            bool result = this.allTasks.Remove(task);
            task.TimeDueChanged -= Task_TimeDueChanged;
            task.TaskChanged -= Task_TaskChanged;
            this.ReorderTasks();
            this.OnCollectionChanged(this, new CollectionChangedEventArgs<TaskBase>()
            {
                ChangedElement = task,
                CollectionChangedType = CollectionChangedType.Remove
            });

            return result;
        }

        private void Task_TimeDueChanged(object sender, EventArgs e)
        {
            this.ReorderTasks();
        }

        private void ReorderTasks()
        {
            this.allTasks.OrderBy(t => t.TimeDue);
        }

        private void Task_TaskChanged(object sender, EventArgs e)
        {
            TaskBase changedTask = sender as TaskBase;
            CollectionChangedEventArgs<TaskBase> collectionChangedEventArgs = new CollectionChangedEventArgs<TaskBase>()
            {
                CollectionChangedType = CollectionChangedType.Update,
                ChangedElement = changedTask
            };

            this.OnCollectionChanged(this, collectionChangedEventArgs);
        }

        private void OnCollectionChanged(object sender, CollectionChangedEventArgs<TaskBase> e)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, e);
            }
        }

        public event EventHandler<CollectionChangedEventArgs<TaskBase>> CollectionChanged;

        public IEnumerator<TaskBase> GetEnumerator()
        {
            return this.allTasks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.allTasks.GetEnumerator();
        }
    }
}
