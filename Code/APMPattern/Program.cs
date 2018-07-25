using System;
using System.Threading;

namespace APMPattern
{
    internal class Program
    {
        private static string LongTask()
        {
            Thread.Sleep(5000);
            return "long_task_result";
        }

        private static void OtherTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("other_task_result");
        }

        private static void Main()
        {
            var asyncTask = new Func<string>(LongTask);
            Console.WriteLine("Start async task.");
            IAsyncResult asyncResult = asyncTask.BeginInvoke(ar =>
            {
                string result = asyncTask.EndInvoke(ar);
                Console.WriteLine("Obtained task result: " + result);
            }, null);

            Console.WriteLine("Perform other tasks.");
            OtherTask();
            asyncResult.AsyncWaitHandle.WaitOne();

            Console.ReadLine();
        }
    }
}