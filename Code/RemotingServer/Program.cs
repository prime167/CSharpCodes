using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace RemotingServer
{
    class RemoteServer
    {
        static void Main(string[] args)
        {
            TcpServerChannel channel = new TcpServerChannel(6666);
            ChannelServices.RegisterChannel(channel,true);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemotingObject.RemoteObject), "RemoteObject", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Press Any Key");
            Console.ReadLine();
        }
    }
}
