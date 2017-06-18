using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Task_5.Helpers;

namespace Task_5.Services
{
    public static class Consumer
    {
        public static void Start()
        {
            var factory = new ConnectionFactory
            {
                HostName = "95.85.3.163",
                Password = "password",
                UserName = "user"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("performance", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                    {
                        var mould = SerializeHelper.ToPerformanceMould(ea.Body);
                        CacheHelper.Save(ea.BasicProperties.AppId, mould);
                    };

                channel.BasicConsume("performance", true, consumer);

                Console.ReadLine();
            }
        }
    }
}