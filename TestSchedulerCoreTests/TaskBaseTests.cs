using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSchedulerCoreTests
{
    [TestClass]
    class TaskBaseTests
    {
        [TestMethod]
        public void CreateTask()
        {
            // Assert name, description and time due, interval, occurrences are empty,
            // time due changed and task changed event should not be fired,
            // task type
        }

        [TestMethod]
        public void ChangeDescription_TaskChangedEventIsFired()
        {
            // task changed event fired
        }
    }
}
