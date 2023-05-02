using System;
using System.Threading;

namespace ThreadPool_dotNET
{
    public class Program
    {
        static void Main(string[] args)
        {
            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"------STRAT (Thread Id-{thread.ManagedThreadId}, Thread Pool-{thread.IsThreadPoolThread}, Background-{thread.IsBackground}, Is Alive-{thread.IsAlive})------\n");

            List<string> urls = new()
            {
                "https://www.google.com/",
                "https://www.duckduckgo.com/",
                "https://www.yahoo.com/",
                "https://www.linkedin.com/",
                "https://www.youtube.com/"
            };
            foreach (var url in urls)
            {
                //ThreadPool.QueueUserWorkItem((state) => CheckHttpStatus(url));
                ThreadPool.QueueUserWorkItem(CheckHttpStatus, url);
            }

            Console.WriteLine($"------END------\n");
            Console.ReadKey();
        }
        static void CheckHttpStatus(string url)
        {
            HttpClient client = new();
            var response = client.GetAsync(url).Result;
            Console.WriteLine($"The HTTP status code of {url} is {response.StatusCode}");

            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"Current Thread Id-{thread.ManagedThreadId}, Thread Pool-{thread.IsThreadPoolThread}, Background-{thread.IsBackground}, Is Alive-{thread.IsAlive}\n");
        }
        static void CheckHttpStatus(object url)
        {
            HttpClient client = new();
            var response = client.GetAsync((string)url).Result;
            Console.WriteLine($"The HTTP status code of {url} is {response.StatusCode}");

            Thread thread = Thread.CurrentThread;
            Console.WriteLine($"Current Thread Id-{thread.ManagedThreadId}, Thread Pool-{thread.IsThreadPoolThread}, Background-{thread.IsBackground}, Is Alive-{thread.IsAlive}\n");
        }

    }
}