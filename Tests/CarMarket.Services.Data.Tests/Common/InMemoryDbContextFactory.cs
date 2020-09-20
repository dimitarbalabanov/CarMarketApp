namespace CarMarket.Services.Data.Tests.Common
{
    using System;

    using CarMarket.Data;

    using Microsoft.EntityFrameworkCore;

    public static class InMemoryDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
