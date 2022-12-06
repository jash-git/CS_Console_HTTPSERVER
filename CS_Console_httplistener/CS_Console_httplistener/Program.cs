using System;
using System.Net;
using System.Text;

namespace CS_Console_httplistener
{
    class Program
    {
        public static HttpListener listener;
        public static int Port = 8001;
        public static int NO = 0;
        static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
        static void Main(string[] args)
        {
            listener = new HttpListener();

            /*
            netsh http add urlacl url=http://+:8001/ user=users
            netsh http show urlacl |findstr "8001"
            netsh http show urlacl
            netsh http delete urlacl url=http://+:8001/
            */
            //listener.Prefixes.Add($"http://localhost:{Port}/");
            //listener.Prefixes.Add($"http://127.0.0.1:{Port}/");
            listener.Prefixes.Add($"http://+:{Port}/");
            listener.Start();
            Console.WriteLine($"Listening on port {Port}...");
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest req = context.Request;

                Console.WriteLine($"Received request for {req.Url}");

                using HttpListenerResponse resp = context.Response;
                resp.ContentType = "application/json";
                resp.ContentEncoding = Encoding.UTF8;

                NO++;
                String data = String.Format("{{\"NO\":\"{0:0000}\",\"En_Name\":\"jash.liao\",\"CH_Name\":\"小廖\"}}", NO);//@"{""NO"":""001"",""En_Name"":""jash.liao"",""CH_Name"":""小廖""}";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                resp.ContentLength64 = buffer.Length;

                using Stream ros = resp.OutputStream;
                ros.Write(buffer, 0, buffer.Length);
            }
            Pause();
        }
    }
}