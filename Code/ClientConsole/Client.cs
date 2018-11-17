using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientConsole
{
    class Client
    {
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            ConsoleKey key;

            ServerClient client = new ServerClient();
            client.SendMessage();

            Console.WriteLine("\n\n输入\"Q\"键退出。");
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Q);
        }
    }
}
