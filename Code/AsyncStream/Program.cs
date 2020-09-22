using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStream
{
    class Program
    {
        private static int ThreadId => Thread.CurrentThread.ManagedThreadId;
        const int Count = 10;

        static async Task Main(string[] args)
        {
            var factory1 = new NumberFactory();
            var factory2 = new AsyncNumberFactory();

            Console.WriteLine(nameof(ThreadId) + $" {ThreadId}");

            foreach (var number in factory1.GenerateNumbers(Count))
            {
                Console.WriteLine($"{ThreadId} {number}");
            }

            Console.WriteLine("====================");
            await foreach (var number in factory2.GenerateNumbers(Count))
            {
                Console.WriteLine($"{ThreadId} {number}");
            }

            Console.WriteLine(nameof(ThreadId) + $" {ThreadId}");
        }
    }

    public abstract class NumberFactoryBase
    {
        public int Delay { get; set; } = 60;
    }

    public class NumberFactory : NumberFactoryBase
    {
        public IEnumerable<int> GenerateNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Task.Delay(Delay).Wait();
                yield return i + 1;
            }
        }
    }

    public class AsyncNumberFactory : NumberFactoryBase
    {
        public async IAsyncEnumerable<int> GenerateNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await Task.Delay(Delay);
                yield return i + 1;
            }
        }
    }
}
