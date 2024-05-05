using System;
using Grpc.Net.Client;
using Grpc.Core;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5271");
            var client = new Customer.CustomerClient(channel);
            var reply = await client.GetCustomerInfoAsync(new CustomerFindModel { UserId = 1 });
            Console.WriteLine($"Customer Info: {reply.FirstName} {reply.LastName}");

            var allCustomers = client.GetAllCustomers(new AllCustomerModel());
            await foreach (var customer in allCustomers.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"Customer Info: {customer.FirstName} {customer.LastName}");
            }
        }
    }
}