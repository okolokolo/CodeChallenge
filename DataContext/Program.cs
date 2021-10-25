using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DataContext
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting connection string...");
            
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            Console.WriteLine("Seeding MenuDb.db...");

            using (var db = new MenuDbContext(connectionString))
            {
                db.SeedData();
            }
        }
    }
}
