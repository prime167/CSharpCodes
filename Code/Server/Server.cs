﻿using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server is running ... ");
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ip, 8500);

            listener.Start();           // 开启对控制端口 8500 的侦听
            Console.WriteLine("Start Listening ...");

            while (true)
            {
                // 获取一个连接，同步方法，在此处中断
                TcpClient client = listener.AcceptTcpClient();
                var wapper = new RemoteClient(client);
                wapper.BeginRead();
            }
        }
    }
}
