using System;
using Hello.Interface;

namespace Hello
{
    public class Hello : IHello
    {
        public void SayHello(string name)
        {
            Console.WriteLine("你好V2 {0}", name);
        }
    }
}
