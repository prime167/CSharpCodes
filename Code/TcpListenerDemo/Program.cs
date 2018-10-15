using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpListenerDemo
{
    public class TcpTimeServer
    {
        private const int PortNum = 13;

        public static int Main(string[] args)
        {
            bool done = false;

            var listener = new TcpListener(IPAddress.Parse("0.0.0.0"), PortNum);

            listener.Start();

            while (!done)
            {
                Console.Write("Waiting for connection...");
                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");
                NetworkStream ns = client.GetStream();

                byte[] byteTime = Encoding.UTF8.GetBytes("大智慧 " + DateTime.Now);

                try
                {
                    ns.Write(byteTime, 0, byteTime.Length);
                    ns.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            listener.Stop();

            return 0;
        }
    }
}