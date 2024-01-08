using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

// Ensure these packages are installed:
// NUnit
// NUnit3TestAdapter
// Microsoft.NET.Test.Sdk
// Microsoft.EntityFrameworkCore.InMemory

namespace TestingLayer
{
    [SetUpFixture]
    public static class SetupFixture
    {
        public static CreatleDbContext dbContext;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            // This code runs before all tests in the assembly
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            dbContext = new CreatleDbContext(builder.Options);

            // Additional setup can be done here, such as seeding the database
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            // This code runs after all tests in the assembly have been run
            dbContext.Dispose();
        }
    }
}