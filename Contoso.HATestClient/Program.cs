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
                DDoSAttack();
            }
            else
            {
                NormallClients();
            }
        }

        private static void NormallClients()
        {
            Parallel.For(0, 100, (i) =>
            {
                var stringTask = client.GetAsync($"http://localhost:8080/?clientId= {i}").GetAwaiter().GetResult();
                Console.WriteLine($"clientId= {i} StatusCode: {stringTask.StatusCode}");
            });
        }

        private static void DDoSAttack()
        {
            for (int i = 0; i < 100; i++)
            {
                var stringTask = client.GetAsync(@"http://localhost:8080/?clientId=3").GetAwaiter().GetResult();
                Console.WriteLine(stringTask.StatusCode);
            }
            //Parallel.For(0, 100, (i) =>
            //{
            //    var stringTask = client.GetAsync(@"http://localhost:8080/?clientId=3").GetAwaiter().GetResult();
            //    Console.WriteLine(stringTask.StatusCode);
            //});
        }
    }
}
