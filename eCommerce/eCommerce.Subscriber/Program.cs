using EasyNetQ;
using eCommerce.Model.Messages;

Console.WriteLine("Hello, World!");

var bus = RabbitHutch.CreateBus("host=localhost");

await bus.PubSub.SubscribeAsync<ProductUpdated>("console_printer", msg =>
{
    Console.WriteLine($"Product {msg.Product.Name} was updated");
    return Task.CompletedTask;
});

Console.WriteLine("Press any key to exit");
Console.ReadKey();