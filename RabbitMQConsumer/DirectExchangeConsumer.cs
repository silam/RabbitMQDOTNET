using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct);

            // declare a queue
            channel.QueueDeclare("demo-direct-queue", durable: true, exclusive: false,
                autoDelete: false, arguments: null);
            // Queue Binding
            channel.QueueBind("demo-direct-queue", "demo-direct-exchange", "account.init");
            // prefetch count
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray(); // this is byte array
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(message);
            };

            channel.BasicConsume("demo-direct-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}
