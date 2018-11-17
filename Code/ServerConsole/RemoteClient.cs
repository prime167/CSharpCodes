﻿using Common;
using System;
using System.Net.Sockets;
using System.Text;

namespace ServerConsole
{
    public class RemoteClient
    {
        private TcpClient client;
        private NetworkStream streamToClient;
        private const int BufferSize = 8192;
        private byte[] buffer;
        private RequestHandler handler;

        public RemoteClient(TcpClient client)
        {
            this.client = client;

            // 打印连接到的客户端信息
            Console.WriteLine("\nClient Connected！{0} <-- {1}",
                client.Client.LocalEndPoint, client.Client.RemoteEndPoint);

            // 获得流
            streamToClient = client.GetStream();
            buffer = new byte[BufferSize];

            // 设置RequestHandler
            handler = new RequestHandler();

            // 在构造函数中就开始准备读取
            var callBack = new AsyncCallback(ReadComplete);
            streamToClient.BeginRead(buffer, 0, BufferSize, callBack, null);
        }

        // 再读取完成时进行回调
        private void ReadComplete(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                lock (streamToClient)
                {
                    bytesRead = streamToClient.EndRead(ar);
                    Console.WriteLine($"Reading data, {bytesRead} bytes ...");
                }
                if (bytesRead == 0) throw new Exception("读取到0字节");

                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                Array.Clear(buffer, 0, buffer.Length);        // 清空缓存，避免脏读

                string[] msgArray = handler.GetActualString(msg);   // 获取实际的字符串

                // 遍历获得到的字符串
                foreach (string m in msgArray)
                {
                    Console.WriteLine($"Received: {m}");

                    // 将得到的字符串改为大写并重新发送
                    string back = m.ToUpper();
                    back = $"[length={back.Length}]{back}";

                    byte[] temp = Encoding.Unicode.GetBytes(back);
                    streamToClient.Write(temp, 0, temp.Length);
                    streamToClient.Flush();
                    Console.WriteLine($"Sent: {back}");
                }

                // 再次调用BeginRead()，完成时调用自身，形成无限循环
                lock (streamToClient)
                {
                    var callBack = new AsyncCallback(ReadComplete);
                    streamToClient.BeginRead(buffer, 0, BufferSize, callBack, null);
                }
            }
            catch (Exception ex)
            {
                streamToClient?.Dispose();
                client.Close();
                Console.WriteLine(ex.Message);      // 捕获异常时退出程序              
            }
        }
    }
}
