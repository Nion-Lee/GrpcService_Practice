using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GrpcService
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

        public override async Task SayHelloStream(HelloRequest request,
            IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = "Hello" + request.Name + " " + i
                });
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
