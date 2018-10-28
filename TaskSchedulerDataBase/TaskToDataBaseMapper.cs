using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSchedulerCore;

namespace TaskSchedulerDataBase
{
    internal class TaskToDataBaseMapper
    {
        Dictionary<TaskDataBaseItem, TaskBase> dataBaseItemToTask;
        Dictionary<TaskBase, TaskDataBaseItem> taskToDataBaseItem;

        public TaskToDataBaseMapper()
        {
            this.dataBaseItemToTask = new Dictionary<TaskDataBaseItem, TaskBase>();
            this.taskToDataBaseItem = new Dictionary<TaskBase, TaskDataBaseItem>();
        }

        public TaskDataBaseItem GetDataBaseItemFromTask(TaskBase task)
        {
            if (!taskToDataBaseItem.ContainsKey(task))
            {
                TaskDataBaseItem newItem = this.CreateNewDataBaseItemFromTaskAccordingToType(task);

                taskToDataBaseItem[task] = newItem;
                dataBaseItemToTask[newItem] = task;
            }

            return taskToDataBaseItem[task];
        }

        private TaskDataBaseItem CreateNewDataBaseItemFromTaskAccordingToType(TaskBase task)
        {
            TaskDataBaseItem newItem = null;

            switch (task.TaskType)
            {
                case TaskType.RecurringTask:

                    RecurringTask recurringTask = task as RecurringTask;
                    newItem = new TaskDataBaseItem()
                    {
                        Name = recurringTask.Name,
                        Description = recurringTask.Description,
                        TaskType = recurringTask.TaskType,
                        TimeDue = recurringTask.TimeDue,
                        Interval = recurringTask.Interval,
                        TaskOccurences = recurringTask.TaskOccurrences.ToArray()
                    };
                    break;

                default:
                    throw new NotSupportedException();
            }

            return newItem;
        }

        public TaskBase GetTaskFromDataBaseItem(TaskDataBaseItem dataBaseItem)
        {
            if (!this.dataBaseItemToTask.ContainsKey(dataBaseItem))
            {
                TaskBase newTask = this.CreateNewTaskAccordingToTaskBaseItem(dataBaseItem);

                taskToDataBaseItem[newTask] = dataBaseItem;
                dataBaseItemToTask[dataBaseItem] = newTask;
            }

            return this.dataBaseItemToTask[dataBaseItem];
        }

        private TaskBase CreateNewTaskAccordingToTaskBaseItem(TaskDataBaseItem dataBaseItem)
        {
            TaskBase newTask = null;

            switch (dataBaseItem.TaskType)
            {
                case TaskType.RecurringTask:
                    RecurringTask newRecurringTask = new RecurringTask(dataBaseItem.Name);
                    newRecurringTask.Description = dataBaseItem.Description;
                    newRecurringTask.Interval = dataBaseItem.Interval;
                    foreach(TaskOccurrence occurrence in dataBaseItem.TaskOccurences)
                    {
                        newRecurringTask.AddOccurrence(occurrence.OccurrenceMoment, occurrence.OccurrenceDuration);
                    }

                    newTask = newRecurringTask;
                    break;

                default:
                    throw new NotSupportedException();
            }

            return newTask;
        }

        public void DeleteItem(TaskDataBaseItem item)
        {
            TaskBase task = this.dataBaseItemToTask[item];
            this.dataBaseItemToTask.Remove(item);
            this.taskToDataBaseItem.Remove(task);
        }

        public void UpdateItemInformation(TaskDataBaseItem dataBaseItem)
        {
            TaskBase task = this.dataBaseItemToTask[dataBaseItem];

            dataBaseItem.Name = task.Name;
            dataBaseItem.Description = task.Description;
            dataBaseItem.TaskType = task.TaskType;
            dataBaseItem.TimeDue = task.TimeDue;

            switch (task.TaskType)
            {
                case TaskType.RecurringTask:

                    RecurringTask recurringTask = task as RecurringTask;
                    dataBaseItem.Interval = recurringTask.Interval;
                    dataBaseItem.TaskOccurences = recurringTask.TaskOccurrences.ToArray();
                    break;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
