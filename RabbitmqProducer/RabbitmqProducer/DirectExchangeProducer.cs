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
    public static class DirectExchangeProducer
    {
        public static void PUblish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            // declare a queue
            channel.ExchangeDeclare("demo-direct-exchange", ExchangeType.Direct, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", message = $"Hello! count : {count}" };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("demo-direct-exchange" // exchange
                                     ,"account.init" // routing key : name of the queue
                                      ,null, // basic property
                                      body);

                count++;

                Thread.Sleep(1000);

            }
        }
    }
}
