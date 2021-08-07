using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitmqProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Factory
            var factory = new ConnectionFactory { 
                Uri = new Uri("amqp://guest:guest@localhost:5672")};
            // Create connection
            using var connection = factory.CreateConnection();
            // create a channel
            using var channel = connection.CreateModel();

            // declare a queue
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false,
                autoDelete: false, arguments: null);

            var message = new { Name = "Producer", message = "Hello!" };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("" // exchange
                                 , "demo-queue" // routing key : name of the queue
                                  , null, // basic property
                                  body              );

        }
    }
}
