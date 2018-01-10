using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using RemotingObject;

namespace RemotingClient
{
    class RemoteClient
    {
        static void Main(string[] args)
        {
            ChannelServices.RegisterChannel(new TcpClientChannel(),true);
            RemoteObject remoteobj = (RemoteObject)Activator.GetObject(typeof(RemoteObject), "tcp://localhost:6666/RemoteObject");
            Console.WriteLine("1+2=" + remoteobj.Add(1, 2));
            Console.ReadLine();
        }
    }
}
