using System;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Task_5.Services;

namespace Task_5
{
    class Program
    {
        static void Main(string[] args)
        {

            Task.Run(() => Consumer.Start());           //берет данные из очереди сообщений
            Task.Run(() => Server.Start());             //к нему обращается клиент для считывания данных из очереди

            //Запускаем отправителей данных о производительности
            Task.Run(() => PerformanceProducer.Start(1, 1.0));
            Thread.Sleep(10000);

            Task.Run(() => PerformanceProducer.Start(2, 0.6));
            Thread.Sleep(10000);

            Task.Run(() => PerformanceProducer.Start(3, 0.3));
            Thread.Sleep(10000);

            Task.Run(() => PerformanceProducer.Start(4, 0.2)); 


            Console.ReadLine();
        }
    }
}
