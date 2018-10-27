using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using TaskSchedulerCore;
using TaskSchedulerCore.Utilities;

namespace TaskSchedulerDataBase
{
    public class DataBaseManager
    {
        private const string TaskCollectionName = "tasks";
        private const string DataBasePath = @"TasksDataBase.db";

        private readonly TaskCollection taskCollection;
        private readonly TaskToDataBaseMapper taskToDataBaseMapper;

        public DataBaseManager(TaskCollection taskCollection)
        {
            this.taskCollection = taskCollection;
            this.taskToDataBaseMapper = new TaskToDataBaseMapper();

            this.SyncInitialData();
            this.taskCollection.CollectionChanged += TaskCollection_CollectionChanged;
        }

        private void SyncInitialData()
        {
            List<TaskBase> allRecordsAddedFromDb = new List<TaskBase>();

            using (var database = new LiteDatabase(DataBasePath))
            {
                var dataBaseTaskCollection = database.GetCollection<TaskDataBaseItem>(TaskCollectionName);

                foreach (TaskDataBaseItem dataBaseItem in dataBaseTaskCollection.FindAll())
                {
                    TaskBase task = this.taskToDataBaseMapper.GetTaskFromDataBaseItem(dataBaseItem);
                    allRecordsAddedFromDb.Add(task);
                    this.taskCollection.AddTask(task);
                }
            }

            foreach (TaskBase task in this.taskCollection)
            {
                if (!allRecordsAddedFromDb.Contains(task))
                {
                    this.AddTask(task);
                }
            }
        }

        private void TaskCollection_CollectionChanged(object sender, CollectionChangedEventArgs<TaskBase> e)
        {
            switch(e.CollectionChangedType)
            {
                case CollectionChangedType.Update:
                    this.UpdateTask(e.ChangedElement);
                    break;
                case CollectionChangedType.Add:
                    this.AddTask(e.ChangedElement);
                    break;
                case CollectionChangedType.Remove:
                    this.RemoveTask(e.ChangedElement);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void AddTask(TaskBase task)
        {
            using (var database = new LiteDatabase(DataBasePath))
            {
                var dataBaseTaskCollection = database.GetCollection<TaskDataBaseItem>(TaskCollectionName);

                TaskDataBaseItem dataBaseItem = taskToDataBaseMapper.GetDataBaseItemFromTask(task);
                dataBaseTaskCollection.Insert(dataBaseItem);
            }
        }

        private void RemoveTask(TaskBase task)
        {
            using (var database = new LiteDatabase(DataBasePath))
            {
                var dataBaseTaskCollection = database.GetCollection<TaskDataBaseItem>(TaskCollectionName);

                TaskDataBaseItem dataBaseItem = taskToDataBaseMapper.GetDataBaseItemFromTask(task);
                taskToDataBaseMapper.DeleteItem(dataBaseItem);
                dataBaseTaskCollection.Delete(t =>  t == dataBaseItem);
            }
        }

        private void UpdateTask(TaskBase task)
        {
            using (var database = new LiteDatabase(DataBasePath))
            {
                var dataBaseTaskCollection = database.GetCollection<TaskDataBaseItem>(TaskCollectionName);
                TaskDataBaseItem dataBaseItem = taskToDataBaseMapper.GetDataBaseItemFromTask(task);
                taskToDataBaseMapper.UpdateItemInformation(dataBaseItem);
                dataBaseTaskCollection.Update(dataBaseItem);
            }
        }
    }
}
