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
    public class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            // declare a queue
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false,
                autoDelete: false, arguments: null);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", message = $"Hello! count : {count}" };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("" // exchange
                                     , "demo-queue" // routing key : name of the queue
                                      , null, // basic property
                                      body);

                count++;

                Thread.Sleep(1000);

            }
            
        }
    }
}
