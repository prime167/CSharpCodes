using System;
using System.Threading;

namespace APMPattern
{
    class Program
    {
        static string LongTask()
        {
            Thread.Sleep(5000);
            return "long_task_result";
        }

        static void OtherTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("other_task_result");
        }

        static void Main()
        {
            var asyncTask = new Func<string>(LongTask);
            Console.WriteLine("Start async task.");
            var asyncResult = asyncTask.BeginInvoke(ar =>
            {
                var result = asyncTask.EndInvoke(ar);
                Console.WriteLine("Obtained task result: " + result);
            }, null);

            Console.WriteLine("Perform other tasks.");
            OtherTask();
            asyncResult.AsyncWaitHandle.WaitOne();

            Console.ReadLine();
        }
    }
}