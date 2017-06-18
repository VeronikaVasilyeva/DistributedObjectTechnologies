using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Task_6.Services
{
    public static class HttpFacade
    {
        public static void Start()
        {
            var listener = new TcpListener(8080);
            listener.Start();

            while (true)
            {
                var tcpClient = listener.AcceptTcpClient();

                var thread = new Thread(ClientThread);
                thread.Start(tcpClient);

                Thread.Sleep(1);
            }
        }

        static void ClientThread(object tcpClient)
        {
            new Handler((TcpClient)tcpClient);
        }
    }

    class Handler
    {
        public Handler(TcpClient tcpClient)
        {
            var request = "";
            var buffer = new byte[1024];
            int count;

            while ((count = tcpClient.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);

                if (request.IndexOf("\r\n\r\n", StringComparison.Ordinal) >= 0 || request.Length > 4096)
                {
                    break;
                }
            }

            var reqMatch = Regex.Match(request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");
            if (reqMatch == Match.Empty)
            {
                SendError(tcpClient, 400);
                return;
            }

            var requestUri = reqMatch.Groups[1].Value;
            requestUri = Uri.UnescapeDataString(requestUri);

            if (string.IsNullOrEmpty(requestUri))
            {
                SendError(tcpClient, 400);
                return;
            }

            var zmqClient = new ZmqClient();
            var bytes = zmqClient.Request(requestUri.Substring(1));

            //var html = "<html><body><h1>It works!</h1></body></html>";
            //var str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + html.Length + "\n\n" + html;
            var header = "HTTP/1.1 200 OK\nContent-type: application/octet-stream\nContent-Length:" + bytes.Length + "\n\n";
            var headerBytes = Encoding.ASCII.GetBytes(header);
            tcpClient.GetStream().Write(headerBytes, 0, headerBytes.Length);
            tcpClient.GetStream().Write(bytes, 0, bytes.Length);
            tcpClient.Close();
        }

        private void SendError(TcpClient tcpClient, int code)
        {
            var codeStr = code + " " + ((HttpStatusCode)code);
            var html = "<html><body><h1>" + codeStr + "</h1></body></html>";
            var str = "HTTP/1.1 " + codeStr + "\nContent-type: text/html\nContent-Length:" + html.Length + "\n\n" + html;
            var buffer = Encoding.ASCII.GetBytes(str);

            tcpClient.GetStream().Write(buffer, 0, buffer.Length);

            tcpClient.Close();
        }
    }
}