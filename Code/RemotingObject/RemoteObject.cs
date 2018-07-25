using System;

namespace RemotingObject
{
    public class RemoteObject : MarshalByRefObject
    {
        public RemoteObject()
        {
            Console.WriteLine("New RemoteObject Added!");
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
