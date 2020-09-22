using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace AspNetCoreGrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayMoreHello(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            var contextCancellationToken = context.CancellationToken;
            foreach (int x in Enumerable.Range(1, 10))
            {
                if (contextCancellationToken.IsCancellationRequested) return;
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name} {x}"
                });

                await Task.Delay(200);
            }
        }

        public override async Task SayMoreHello1(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            var helloQueue = new Queue<string>();
            while (await requestStream.MoveNext())
            {
                helloQueue.Enqueue(requestStream.Current.Name);
                Console.WriteLine(requestStream.Current.Name);
            }

            //遍历队列开始安检
            while (helloQueue.TryDequeue(out string name))
            {
                if (name.Contains("5"))
                {
                    await responseStream.WriteAsync(new HelloReply { Message = $"{name} not OK" });
                }
                else
                {
                    await responseStream.WriteAsync(new HelloReply { Message = $"{name} OK" });
                }

                await Task.Delay(500);
            }
        }
    }
}
