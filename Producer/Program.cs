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

var message ="This my first message in rabbitMQ";

var encodingMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish("","LetterBox", null, encodingMessage);
Console.WriteLine($"Published message: {message}");