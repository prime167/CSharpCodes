using System;
using System.Net.Sockets;
using System.Text;

namespace TcpClientDemo
{
    public class TcpTimeClient
    {
        private const int PortNum = 13;
        private const string HostName = "127.0.0.1";

        public static void Main(String[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    var client = new TcpClient(HostName, PortNum);
                    client.NoDelay = true;
                    NetworkStream ns = client.GetStream();

                    byte[] bytes = new byte[1024];
                    int bytesRead = ns.Read(bytes, 0, bytes.Length);

                    Console.WriteLine(Encoding.UTF8.GetString(bytes, 0, bytesRead));

                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            Console.ReadLine();
        }
    }
}