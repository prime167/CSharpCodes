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
            foreach (var x in Enumerable.Range(1, 10))
            {
                if (contextCancellationToken.IsCancellationRequested) return;
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name} {x}"
                });

                await Task.Delay(200);
            }
        }
    }
}
