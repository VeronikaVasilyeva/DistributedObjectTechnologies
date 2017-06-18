using System;
using System.Threading.Tasks;
using Task_6.Services;

namespace Task_6
{
    class Program
    {
        public static void Main()
        {
            Task.Run(() => ZmqBroker.Start());

            Task.Run(() => ZmqWorker.Start(1));
            Task.Run(() => ZmqWorker.Start(2));
            Task.Run(() => ZmqWorker.Start(3));
            Task.Run(() => ZmqWorker.Start(4));

            Task.Run(() => HttpFacade.Start());

            Console.ReadLine();
        }
    }
}
