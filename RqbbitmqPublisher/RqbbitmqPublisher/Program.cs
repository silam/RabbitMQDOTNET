using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RqbbitmqPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostname = "localhost";

            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri($"amqp://transcode_user:password@{hostname}/video.transcode.vhost"),

            };

            var exchange = "videoreceived.exchange";
            using (var connection = connectionFactory.CreateConnection()) 
            using (var channel = connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();

                properties.Persistent = false;

                var propertiesDictionary = new Dictionary<string, object>();

                properties.Headers = propertiesDictionary;

                propertiesDictionary.Add("app_id", "Matius Video Site");
                propertiesDictionary.Add("content_type", "text/plain");

                do
                {
                    propertiesDictionary["message_id"] = Guid.NewGuid().ToString("N");
                    var messageBody = GetMessageBody();

                    var body = Encoding.UTF8.GetBytes(messageBody);

                    channel.BasicPublish(exchange: exchange, routingKey: string.Empty, properties, body: body);
                    Console.WriteLine($"Send Message: {messageBody}");
                    Console.ReadLine();

                } while (true);


            }

        }

        private static string GetMessageBody()
        {
            return $"date_time: { DateTime.UtcNow.ToString()}" + $"\r\ndata: this is the body of the message";
        }
    }
}
