using System;
using System.Globalization; 
using System.Linq;

namespace CSharpAsFunctionProgramingLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> action = Console.WriteLine;
            Action<string> hello = Hello;
            Action<string> goodbye = Goodbye;
            action += Hello;
            action += (x) => Console.WriteLine("  Greating {0} from lambda expression", x);
            action("First"); // called WriteLine, Hello, and lambda expression

            action -= hello;
            action("Second"); // called WriteLine, and lambda expression

            action = Console.WriteLine + goodbye
                    + delegate(string x)
            {
                Console.WriteLine("  Greating {0} from delegate", x);
            };
            action("Third");// called WriteLine, Goodbye, and delegate

            (action - Goodbye)("Fourth"); // called WriteLine and delegate
            Console.WriteLine("methods in action:");
            action.GetInvocationList().ToList().ForEach(del => Console.WriteLine(del.Method.Name));
            Console.WriteLine("===");
            Console.WriteLine("delete add and remove");
            Action a = () => Console.Write("A");
            Action b = () => Console.Write("B");
            Action c = () => Console.Write("C");
            Action s = a + b + c + Console.WriteLine;
            s();                  //ABC
            (s - a)();            //BC
            (s - b)();            //AC
            (s - c)();            //AB
            (s - (a + b))();      //C
            (s - (b + c))();      //A 
            (s - (a + c))();      //ABC                    

            s = a + b + a;
            (s - a)();

            Console.WriteLine("=====");
            string[] words = { "C#", ".NET", "ASP.NET", "MVC", "jack", "Visual Studio" };
            Func<string, char> firstLetter = ss => ss[0];
            var sorted = words.OrderBy(word => word.Length).ThenBy(firstLetter).ToList();
            sorted.ForEach(Console.WriteLine);

            Console.WriteLine("===");
            Func<double, double> sin = Math.Sin;
            Func<double, double> exp = Math.Exp;
            Func<double, double> exp_sin = Compose(sin, exp);
            double y = exp_sin(3);
            Console.WriteLine(y);
            Console.ReadLine();

        }

        static Func<X, Z> Compose<X, Y, Z>(Func<X, Y> f, Func<Y, Z> g)
        {
            return (x) => g(f(x));
        }

        static void Hello(string s)
        {
            System.Console.WriteLine("  Hello, {0}!", s);
        }

        static void HelloK(string s)
        {
            System.Console.WriteLine("  Hellok, {0}!", s);
        }

        static void Goodbye(string s)
        {
            System.Console.WriteLine("  Goodbye, {0}!", s);
        }
    }
}
