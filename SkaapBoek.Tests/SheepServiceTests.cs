using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NUnit.Framework;
using SkaapBoek.Core;
using SkaapBoek.DAL;
using SkaapBoek.DAL.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.Tests
{
    public class SheepServiceTests
    {
        private DbContextOptions<AppDbContext> Options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "skaap_boek_test")
        .Options;

        [OneTimeSetUp]
        public void Setup()
        {
            DbInitializer.Initialize(new AppDbContext(Options));
        }

        [Test]
        public async Task GetParentChildrenAfterRunningUpdateChildren()
        {
            using var context = new AppDbContext(Options);
            ISheepService service = new SheepService(context);

            var parent = await service.GetById(1);
            await service.UpdateChildren(parent, new[] { 5, 6 });
            var child = await service.GetById(5);
            var child2 = await service.GetById(6);

            Assert.IsTrue(child.FatherId == 1);
            Assert.IsTrue(child2.FatherId == 1);
        }
    }
}