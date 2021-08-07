using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Factory
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            // Create connection
            using var connection = factory.CreateConnection();
            // create a channel
            using var channel = connection.CreateModel();

            // declare a queue
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false,
                autoDelete: false, arguments: null);


            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray(); // this is byte array
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-queue", true, consumer);

            Console.ReadLine();
        }
    }
}
