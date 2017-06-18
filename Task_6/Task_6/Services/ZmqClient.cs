using ZeroMQ;

namespace Task_6.Services
{
    public class ZmqClient
    {
        public byte[] Request(string uri)
        {
            using (var requester = new ZSocket(ZSocketType.REQ))
            {
                requester.Connect("tcp://localhost:8081");

                requester.Send(new ZFrame(uri));

                using (var frame = requester.ReceiveFrame())
                {
                    return frame.Read();
                }
            }
        }
    }
}
