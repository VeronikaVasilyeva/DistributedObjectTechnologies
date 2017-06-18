using System;
using System.IO;
using System.Threading;
using ZeroMQ;

namespace Task_6.Services
{
    public static class ZmqWorker
    {
        public static void Start(int id)
        {
            using (var responder = new ZSocket(ZSocketType.REP))
            {
                responder.Connect("tcp://localhost:8082");

                while (true)
                {
                    using (var frame = responder.ReceiveFrame())
                    {
                        Console.WriteLine($"{id} - {DateTime.Now.ToString("HH:mm:ss")}");
                        Thread.Sleep(10000);

                        var filepath = frame.ReadString();

                        var bytes = new byte[0];
                        if (File.Exists(filepath))
                        {
                            bytes = File.ReadAllBytes(filepath);
                        }

                        responder.Send(new ZFrame(bytes));

                        Thread.Sleep(1);
                    }
                }
            }
        }
    }
}