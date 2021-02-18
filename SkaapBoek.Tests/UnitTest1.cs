using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NUnit.Framework;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;

namespace SkaapBoek.Tests
{
    public class Tests
    {
        public DbContextOptions<AppDbContext> Options { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            Options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(config.GetConnectionString("SkaapBoekDb"))
                .Options;
        }

        [Test]
        public void Test1()
        {
            var context = new AppDbContext(Options);
            ISheepService service = new SheepService(context);

            //var result = service.GetAvailableChildren();
        }
    }
}