using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Contoso.HATestClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Press 1 for DDosAttack, Press 2 for normal users");
            var input = Console.ReadLine();
            if (input.Equals("1"))
            {
                DDoSAttack2();
            }
            else
            {
                NormallClients();
            }
            Console.ReadLine();
        }

        private static void NormallClients()
        {
            Parallel.For(0, 100, async (i) =>
            {
                var stringTask = await client.GetAsync($"http://localhost:8080/?clientId= {i}");
                Console.WriteLine($"clientId= {i} StatusCode: {stringTask.StatusCode}");
            });
        }

        private static void DDoSAttack()
        {
            Parallel.For(0, 100, async (i) =>
            {
                var stringTask =  client.GetAsync(@"http://localhost:8080/?clientId=3").GetAwaiter().GetResult();
                Console.WriteLine(stringTask.StatusCode);
            });
        }

        private static void DDoSAttack2()
        {
            Task[] tasks = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                tasks[i] = Task.Factory.StartNew(async() =>
                {
                    var now = DateTime.Now;
                    var stringTask = await client.GetAsync(@"http://localhost:8080/?clientId=3");
                    Console.WriteLine($"{now} {stringTask.StatusCode} + {System.Threading.Thread.CurrentThread.ManagedThreadId} ");
                });
            }
            Task.WaitAll(tasks);
        }
    }
}
