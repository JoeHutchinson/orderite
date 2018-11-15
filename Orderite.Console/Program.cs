using System;
using MyNamespace;

namespace Orderite.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello");

            var client = new BasketsClient("http://localhost:50331");
            var result = client.CreateBasketAsync("joe", new CreateBasket() {BasketId = 1}).GetAwaiter().GetResult();

            System.Console.WriteLine($"Result : {string.Join(",", result)}");
            System.Console.ReadKey();
        }
    }
}
