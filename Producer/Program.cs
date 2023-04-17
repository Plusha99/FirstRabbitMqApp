using System;
using System.Text;
using RabbitMQ.Client;


var factory = new ConnectionFactory{HostName = "localhost"};

using var conn = factory.CreateConnection();

using var channel = conn.CreateModel();

channel.QueueDeclare(queue:"LetterBox",
    durable:false,
    exclusive:false,
    autoDelete:false,
    arguments:null);

var random = new Random();
var messageId = 1;

while (true)
{
    var publishingTime = random.Next(1,4);
    var message = $"Sending MessageId: {messageId}";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("","LetterBox", null, body);
    Console.WriteLine($"Send message: {message}"); 

    Task.Delay(TimeSpan.FromSeconds(publishingTime)).Wait();
    messageId++;
}
