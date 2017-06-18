using System;
using System.Net;
using System.Threading;
using Task_5.Helpers;

namespace Task_5.Services
{
    public static class Server
    {
        public static void Start()
        {
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
                    if (context.Request.HttpMethod == "GET")
                    {
                        var cache = CacheHelper.Get();
                        cache.ToJson(context.Response.OutputStream);
                        context.Response.ContentType = "application/json";
                        context.Response.OutputStream.Close();
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
