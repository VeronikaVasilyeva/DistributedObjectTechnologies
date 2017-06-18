using System;
using System.Net;
using System.Threading;
using OpenGraph;
using Server.Implementations;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            var handler = new OpenGraphHandler();
            var processor = new OpenGraphService.Processor(handler);
            var httpHanlder = new THttpHandler(processor, new TJSONProtocol.Factory());

            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            Console.WriteLine("Starting the server...");
            listener.Start();

            while (true)
            {
                if (listener.IsListening)
                {
                    var context = listener.GetContext();
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    context.Response.AddHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE");
                    context.Response.AddHeader("Access-Control-Allow-Headers", "Accept, Content-type");

                    if (context.Request.HttpMethod == "OPTIONS")
                    {
                        context.Response.OutputStream.Close();
                    }
                    else
                    {
                        httpHanlder.ProcessRequest(context);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}