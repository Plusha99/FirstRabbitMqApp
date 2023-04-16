using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory{HostName = "localhost"};

using var conn = factory.CreateConnection();

using var channel = conn.CreateModel();

channel.QueueDeclare(queue:"LetterBox",
    durable:false,
    exclusive:false,
    autoDelete:false,
    arguments:null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    System.Console.WriteLine($"Message received: {message}");
};

channel.BasicConsume(queue:"LetterBox", autoAck:true, consumer:consumer);

Console.ReadKey();
