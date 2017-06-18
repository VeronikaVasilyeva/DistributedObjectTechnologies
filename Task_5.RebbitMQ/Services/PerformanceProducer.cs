using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;
using Task_5.Helpers;

namespace Task_5.Services
{
    public static class PerformanceProducer
    {
        public static void Start(int id, double coef)
        {
            var factory = new ConnectionFactory
            {
                HostName = "95.85.3.163",
                Password = "password",
                UserName = "user"
            };
            var properties = new BasicProperties { AppId = $"#{id}" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("performance", false, false, false, null);

                while (true)
                {
                    var mould = PerformanceHelper.GetMould(coef);
                    var bytes = mould.ToBytes();
                    channel.BasicPublish("", "performance", properties, bytes);

                    Thread.Sleep(1000);
                }
            }
        }
    }
}