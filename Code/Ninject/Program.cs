// http://www.dimecasts.net/Content/WatchEpisode/27

using System;

namespace Ninject
{
    class Program
    {
        static void Main(string[] args)
        {
            IDrink drink = new Milk();
            IEditor editor = new Vi();
            var programmer = new LinuxProgrammer(drink,editor);
            programmer.WriteCode();
            Console.WriteLine();

            // using ninject
            var module = new CustomModule();
            IKernel kernel = new StandardKernel(module);

            var p = kernel.Get<MicrosoftProgrammer>(); 
            p.WriteCode(); // only use visual studio

            Console.WriteLine();
            var p1 = kernel.Get<LinuxProgrammer>(); 
            p1.WriteCode(); // only use Vi

            Console.ReadLine();
        }
    }
}
