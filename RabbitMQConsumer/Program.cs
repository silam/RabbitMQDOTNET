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

            DirectExchangeConsumer.Consume(channel);
        }
    }
}
