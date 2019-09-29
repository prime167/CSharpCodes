using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreGrpcService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;

namespace AspNetCoreGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "grpc" });
            Console.WriteLine("Greeter 服务返回数据: " + reply.Message);

            var tokenSource = new CancellationTokenSource();
            var n = 0;
            var replies = client.SayMoreHello(new HelloRequest { Name = "Laurent" });

            try
            {
                await foreach (HelloReply rep in replies.ResponseStream.ReadAllAsync(tokenSource.Token))
                {
                    Console.WriteLine(rep.Message);
                    if (++n == 5)
                    {
                        tokenSource.Cancel();
                    }
                }
            }
            catch (RpcException e) when (e.Status.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Streaming was cancelled from the client!");
            }

            var catClient = new LuCat.LuCatClient(channel);
            var catReply = await catClient.SuckingCatAsync(new Empty());
            Console.WriteLine("调用撸猫服务：" + catReply.Message);
            Console.ReadKey();
        }
    }
}
