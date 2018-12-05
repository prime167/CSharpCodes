using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your Server Ip: ");
            string ip = Console.ReadLine();

            Console.WriteLine("Connecting to server");
            var client = new ServerClient(ip, 8500);
            Console.WriteLine("Connected to server");
            string input;
            string path = Environment.CurrentDirectory + "/";

            do
            {
                Console.WriteLine("Send File:\n\tS1 - Client01.jpg\n\tS2 - Client02.jpg\n\tS3 - Client03.jpg");
                Console.WriteLine("\nReceive File:\n\tR1 - Server01.jpg,\n\tR2 - Server02.jpg, \n\tR3- Server03.jpg");
                Console.WriteLine("Press 'Q' to exit. \n");
                Console.Write("Enter your choice: ");
                input = Console.ReadLine();
                switch (input?.ToUpper())
                {
                    case "S1":
                        client.BeginSendFile(path + "Client01.jpg");
                        break;

                    case "S2":
                        client.BeginSendFile(path + "Client02.jpg");
                        break;

                    case "S3":
                        client.BeginSendFile(path + "Client03.jpg");
                        break;

                    case "R1":
                        client.BeginReceiveFile("Server01.jpg");
                        break;

                    case "R2":
                        client.BeginReceiveFile("Server02.jpg");
                        break;

                    case "R3":
                        client.BeginReceiveFile("Server03.jpg");
                        break;
                }
            } while (input.ToUpper() != "Q");

            client.Dispose();
        }
    }
}
