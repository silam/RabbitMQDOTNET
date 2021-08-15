using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitmqProducer
{
    public static class HeaderExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            // declare a queue
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", message = $"Hello! count : {count}" };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();

                properties.Headers = new Dictionary<string, object> { { "acount", "new" } };

                channel.BasicPublish("demo-header-exchange" // exchange
                                     ,string.Empty// routing key : name of the queue
                                      , properties, // basic property
                                      body);

                count++;

                Thread.Sleep(1000);

            }
        }
    }
}
