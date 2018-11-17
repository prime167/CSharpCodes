using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerConsole
{
    class Server
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server is running ... ");
            TcpListener listener = new TcpListener(IPAddress.Any, 8500);

            listener.Start();           // 开始侦听
            Console.WriteLine("Start Listening ...");

            while (true)
            {
                // 获取一个连接，同步方法，在此处中断
                TcpClient client = listener.AcceptTcpClient();
                RemoteClient wapper = new RemoteClient(client);
            }
        }
    }
}
