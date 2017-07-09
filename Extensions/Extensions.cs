using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobTimer.Model;

namespace JobTimer
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }
            /* If this is a list, use the Count property for efficiency. 
             * The Count property is O(1) while IEnumerable.Count() is O(N). */
            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }

        //public static TaskModelLocal CovertToLocal(this TaskModel arg)
        //{
        //    return new TaskModelLocal()
        //    {
        //        TaskName = arg.TaskName,
        //        Description = arg.Description,
        //        EndTime = arg.EndTime,
        //        StartTime = arg.EndTime,
        //        TotalTime = arg.TotalTime,
        //    };
        //}


        //public static TaskModel CovertToWeb(this TaskModelLocal arg)
        //{
        //    return new TaskModel()
        //    {
        //        TaskName = arg.TaskName,
        //        Description = arg.Description,
        //        EndTime = arg.EndTime,
        //        StartTime = arg.EndTime,
        //        TotalTime = arg.TotalTime,
        //    };
        //}
    }
}
