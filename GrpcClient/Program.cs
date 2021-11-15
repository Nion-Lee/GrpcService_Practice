using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var client = new Greeter.GreeterClient(channel);

            var response = await client.SayHelloAsync(new HelloRequest
            {
                Name = "Nion微帥"
            });

            Console.WriteLine("From people: " + response.Message);

            var call = client.SayHelloStream(new HelloRequest
            {
                Name = ".NET Conf streaming"
            });

            await foreach (var item in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine("From server: " + item.Message);
            }
        }
    }
}
