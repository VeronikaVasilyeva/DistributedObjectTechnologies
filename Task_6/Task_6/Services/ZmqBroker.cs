using System;
using ZeroMQ;

namespace Task_6.Services
{
    internal class ZmqBroker
    {
        public static void Start()
        {
            using (var ctx = new ZContext())
            using (var frontend = new ZSocket(ctx, ZSocketType.ROUTER))
            using (var backend = new ZSocket(ctx, ZSocketType.DEALER))
            {
                frontend.Bind("tcp://*:8081");
                backend.Bind("tcp://*:8082");

                var poll = ZPollItem.CreateReceiver();

                while (true)
                {
                    ZError error;
                    ZMessage message;

                    if (frontend.PollIn(poll, out message, out error, TimeSpan.FromMilliseconds(64)))
                    {
                        backend.Send(message);
                    }
                    else
                    {
                        if (error == ZError.ETERM)
                            return;
                        if (error != ZError.EAGAIN)
                            throw new ZException(error);
                    }

                    if (backend.PollIn(poll, out message, out error, TimeSpan.FromMilliseconds(64)))
                    {
                        frontend.Send(message);
                    }
                    else
                    {
                        if (error == ZError.ETERM)
                            return;
                        if (error != ZError.EAGAIN)
                            throw new ZException(error);
                    }
                }
            }
        }
    }
}
