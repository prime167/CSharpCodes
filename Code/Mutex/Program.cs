using System;
using System.Threading;

namespace Mutex_
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool createdNew;

            Mutex m = new Mutex(true, "myApp", out createdNew);

            if (!createdNew)
            {
                // myApp is already running...
                Console.WriteLine("myApp is already running!");
                Environment.Exit(0);

            }

            Console.WriteLine("hello");
            Console.ReadLine();
        }
    }
}
