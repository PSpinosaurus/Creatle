using BusinessLayer;
using DataLayer;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingLayer
{
    [TestFixture]
    public class CategoriesContextTest
    {
        private CategoriesContext context = new CategoriesContext(SetupFixture.dbContext);
        private Categories category;
        private Answer answer1, answer2;

        [SetUp]
        public async Task CreateCategoryAsync()
        {
            category = new Categories("Fiction");

            answer1 = new Answer(DateTime.Now, 1, category.Id);
            answer2 = new Answer(DateTime.Now.AddYears(1), 2, category.Id);

            category.Answers.Add(answer1);
            category.Answers.Add(answer2);

            await context.CreateAsync(category);
        }

        [TearDown]
        public async Task DropCategoryAsync()
        {
            foreach (var item in SetupFixture.dbContext.Categories)
            {
                SetupFixture.dbContext.Categories.Remove(item);
            }

            await SetupFixture.dbContext.SaveChangesAsync();
        }

        [Test]
        public async Task CreateAsync()
        {
            // Arrange
            Categories newCategory = new Categories("Non-Fiction");

            // Act
            int categoriesBefore = SetupFixture.dbContext.Categories.Count();
            await context.CreateAsync(newCategory);

            // Assert
            int categoriesAfter = SetupFixture.dbContext.Categories.Count();
            Assert.IsTrue(categoriesBefore + 1 == categoriesAfter, "CreateAsync() does not work!");
        }

        [Test]
        public async Task ReadAsync()
        {
            Categories readCategory = await context.ReadAsync(category.Id, false, false);

            Assert.AreEqual(category, readCategory, "ReadAsync does not return the same object!");
        }

        [Test]
        public async Task ReadAllAsync()
        {
            List<Categories> categories = (List<Categories>)await context.ReadAllAsync();

            Assert.That(categories.Count != 0, "ReadAllAsync() does not return categories!");
        }

        [Test]
        public async Task UpdateAsync()
        {
            Categories changedCategory = await context.ReadAsync(category.Id, false, false);

            changedCategory.Name = "Updated " + category.Name;

            await context.UpdateAsync(changedCategory);

            // Read the category from the db! Important for dbContexts with shorter lifespan!
            category = await context.ReadAsync(category.Id, false, false);

            Assert.AreEqual(changedCategory, category, "UpdateAsync() does not work!");
        }

        [Test]
        public async Task DeleteAsync()
        {
            int categoriesBefore = SetupFixture.dbContext.Categories.Count();

            await context.DeleteAsync(category.Id);
            int categoriesAfter = SetupFixture.dbContext.Categories.Count();

            Assert.IsTrue(categoriesBefore - 1 == categoriesAfter, "DeleteAsync() does not work! 👎🏻");
        }

    }
}