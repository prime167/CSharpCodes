using System;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton2 singleton = Singleton2.Instance;
            Parallel.For(1, 4, a => { 
                singleton.Add();
                Console.WriteLine($"Add from thread {Thread.CurrentThread.ManagedThreadId}");
            });

            Console.WriteLine("counter:{0}", singleton.GetCounter());
            Console.ReadLine();
        }
    }
}
