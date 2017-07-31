using System;

using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using SampleOrdering.Domain.Entities;
using SampleOrdering.Facades;

namespace SampleOrdering.Client
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        const string HelpText = @"
        For execute action enter number of action
        Available actions:
        1. Buy Shoes, costs 5$.
        2. Buy Hat, costs 10$.
        3. Buy T-shirt, costs 22$
        4. Increment balance by 10$.
        5. Exit from application.";

        static void Main(string[] args)
        {
            // Then configure and connect a client.
            var clientConfig = ClientConfiguration.LocalhostSilo();
            var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
            client.Connect().Wait();

            Console.WriteLine("Client connected.");

            //
            // This is the place for your test code.
            //

            var products = new[]
            {
                new Product { Id = Guid.Parse("20bf1e2e-76ec-4ed2-8ebe-9d1856a277b7"), CurrentPrice = 5, Description = "Shoes for beach.", StockId = Guid.Parse("8da4f6d4-6fe2-47e9-9cac-47b94ff1808e"), Title = "Shoes" },
                new Product { Id = Guid.Parse("f9630706-3c1f-4fc2-b25d-ff9d7dacd44d"), CurrentPrice = 10, Description = "Hat for gentelmens from London.", StockId = Guid.Parse("3e451920-495e-48cf-889b-7767986b4960"), Title = "Hat" },
                new Product { Id = Guid.Parse("9d702e3d-bf87-47d3-bc37-9866bb028556"), CurrentPrice = 22, Description = "T-shirt for summer holidays, when you relaxing on a beach.", StockId = Guid.Parse("390c5783-4704-44bc-bbe4-2909b266a004"), Title = "T-shirt" }
            };

            foreach (var product in products)
            {
                // activating grain in silos.
                var productGrain = client.GetGrain<IProductGrain>(product.Id);
                productGrain.SetAttributes(new ProductAttributes
                {
                    CurrentPrice = product.CurrentPrice,
                    Description = product.Description,
                    Id = product.Id,
                    StockId = product.StockId,
                    Title = product.Title
                });
            }

            // Bootstrapping first client.

            var clientGrain = client.GetGrain<IClientGrain>(Guid.NewGuid());
            var balanceId = clientGrain.GetBalanceId().Result;

            var balanceGrain = client.GetGrain<IBalanceGrain>(balanceId);
            balanceGrain.Increase(1200);
            var exit = false;

            while (!exit)
            {
                Console.WriteLine("----------------- MS Orleans Sample -----------------");
                Console.WriteLine(HelpText);

                Console.WriteLine($"Current balance is {clientGrain.GetBalance().Result}");
                Console.WriteLine($"Current order price is {clientGrain.GetOrderPrice().Result}");

                var enteredPositionRaw = Console.ReadLine();

                int.TryParse(enteredPositionRaw, out var enteredPosition);

                // Add exception handling when we exhaust stock of product.

                switch (enteredPosition)
                {
                    case 1:
                        clientGrain.PlaceProduct(products[0].Id, 1);

                        break;
                    case 2:

                        clientGrain.PlaceProduct(products[1].Id, 1);
                        break;
                    case 3:

                        clientGrain.PlaceProduct(products[2].Id, 1);
                        break;
                    case 4:

                        balanceGrain.Increase(10);
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Can't find position in options.");
                        break;
                }
            }


            Console.WriteLine("\nPress Enter to terminate...");
            Console.ReadLine();

            // Shut down
            client.Close();
        }
    }
}
