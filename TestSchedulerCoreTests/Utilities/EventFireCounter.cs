using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSchedulerCoreTests.Utilities
{
    public class EventFireCounter
    {
        private int counter;

        public EventFireCounter(Action<EventHandler> subscribeAction)
        {
            subscribeAction(this.Handler);
        }

        public int FireCount
        {
            get
            {
                return this.counter;
            }
        }

        public void Handler(object sender, EventArgs e)
        {
            counter++;
        }
    }
}
