using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStream
{
    class Program
    {
        private static int ThreadId => Thread.CurrentThread.ManagedThreadId;

        static async Task Main(string[] args)
        {
            //var factory = new NumberFactory();
            var factory = new AsyncNumberFactory();

            Console.WriteLine(nameof(ThreadId) +$" {ThreadId}");
            await foreach (var number in factory.GenerateNumbers(30))
            {
                Console.WriteLine($"{ThreadId} {number}");
            }

            Console.WriteLine(nameof(ThreadId) + $" {ThreadId}");
        }
    }

    public class NumberFactory
    {
        public IEnumerable<int> GenerateNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Task.Delay(1000).Wait();
                yield return i + 1;
            }
        }
    }

    public class AsyncNumberFactory
    {
        public async IAsyncEnumerable<int> GenerateNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(1000);
                yield return i + 1;
            }
        }
    }
}
