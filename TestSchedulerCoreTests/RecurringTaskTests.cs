using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskSchedulerCore;
using TestSchedulerCoreTests.Utilities;

namespace TestSchedulerCoreTests
{
    [TestClass]
    public class RecurringTaskTests
    {
        [TestMethod]
        public void CreateRecurringTask_TaskTypeIsCorrect()
        {
            RecurringTask task = new RecurringTask("Wash dishes.");

            Assert.AreEqual(TaskType.RecurringTask, task.TaskType);
        }

        [TestMethod]
        public void AddOccurencesToRecurringTask_TaskChangedAndTimeDueChangedEventsFired()
        {
            RecurringTask task = new RecurringTask("Wash dishes.");

            EventFireCounter taskChangedCounter = new EventFireCounter((h) =>
            {
                task.TaskChanged += h;
            });

            EventFireCounter timeDueCounter = new EventFireCounter((h) =>
            {
                task.TimeDueChanged += h;
            });

            DateTime occurrenceTime = DateTime.Now;
            task.AddOccurrence(occurrenceTime, new TimeSpan(0, 20, 0));

            Assert.AreEqual(occurrenceTime, task.TimeDue);
            Assert.AreEqual(2, taskChangedCounter.FireCount);
            Assert.AreEqual(1, timeDueCounter.FireCount);
        }


        [TestMethod]
        public void CreateTaskWithTimeDueBeforePresentMoment_TaskIsOverdue()
        {
            RecurringTask task = new RecurringTask("Wash dishes.");
            task.AddOccurrence(new DateTime(1986, 4, 2), new TimeSpan(0, 20, 0));

            Assert.IsTrue(task.TaskIsOverdue);
        }

        [TestMethod]
        public void ChangeIntervalOfRecurringTask_TaskChangedAndTimeDueChangedEventsFired()
        {
            RecurringTask task = new RecurringTask("Wash dishes.");

            EventFireCounter taskChangedCounter = new EventFireCounter((h) =>
            {
                task.TaskChanged += h;
            });

            EventFireCounter timeDueCounter = new EventFireCounter((h) =>
            {
                task.TimeDueChanged += h;
            });

            task.Interval = new TimeSpan(24, 0, 0);

            Assert.AreEqual(2, taskChangedCounter.FireCount);
            Assert.AreEqual(1, timeDueCounter.FireCount);
        }
    }
}
