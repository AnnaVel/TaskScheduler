using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSchedulerCore.Utilities
{
    public static class HelperMethods
    {
        public static bool CollectionEqualsCollection<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            int firstCount = first.Count();
            int secondCount = second.Count();

            bool result = firstCount == secondCount;

            if(!result)
            {
                return result;
            }

            for(int i = 0; i < firstCount; i++)
            {
                var firstItem = first.ElementAt(i);
                var secondItem = second.ElementAt(i);

                result &= firstItem.Equals(secondItem);
            }

            return result;
        }
    }
}
