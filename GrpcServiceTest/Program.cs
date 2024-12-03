using Grpc.Net.Client;
using GrpcServiceTest;
using GrpcServiceTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GreeterService>()
    .RequireHost("*:5128")
    .AllowAnonymous();

app.MapGet("/", async () =>
{
    using var channel = GrpcChannel.ForAddress("https://codefactors-grpc-test.azurewebsites.net:5128");
    var client = new Greeter.GreeterClient(channel);
    var reply = await client.SayHelloAsync(
                      new HelloRequest { Name = "GreeterClient1" });
    return reply;
});

app.Run();
