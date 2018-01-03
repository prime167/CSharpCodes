using System;
using Hello.Interface;

namespace Hello
{
    public class Hello:IHello
    {
        public void SayHello(string name)
        {
            Console.WriteLine("Hello {0}", name);
        }
    }
}
