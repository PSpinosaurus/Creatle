using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoriesContext : IDb<Categories, int>
    {
        private readonly CreatleDbContext dbContext;
        public CategoriesContext(CreatleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Categories item)
        {
            try
            {
                List<Answer> answers = new List<Answer>(item.Answers.Count);
                object[] keys = new object[3];
                foreach (Answer answer in item.Answers)
                {
                    keys = new object[] { answer.Date, answer.CategoryId, answer.GameId };
                    Answer answerFromDb = await dbContext.Answers.FindAsync(keys);

                    if (answerFromDb == null)
                    {
                        answers.Add(answer);
                    }
                    else
                    {
                        answers.Add(answerFromDb);
                    }
                }

                List<CategoriesValues> categoriesvalues = new List<CategoriesValues>(item.CategoriesValues.Count);

                foreach (CategoriesValues category in item.CategoriesValues)
                {
                    CategoriesValues categoriesValuesFromDb = await dbContext.CategoriesValues.FindAsync(category.Id);

                    if (categoriesValuesFromDb == null)
                    {
                        categoriesvalues.Add(category);
                    }
                    else
                    {
                        categoriesvalues.Add(categoriesValuesFromDb);
                    }
                }

                List<HeroProfile> heroprofiles = new List<HeroProfile>(item.HeroProfiles.Count);
                foreach (HeroProfile heroProfile in item.HeroProfiles)
                {
                    keys = new object[] { heroProfile.ValueId, heroProfile.GameId, heroProfile.HeroId, heroProfile.CategoryId };
                    HeroProfile heroProfileFromDb = await dbContext.HeroProfiles.FindAsync(keys);
                    
                    if (heroProfileFromDb == null)
                    {
                        heroprofiles.Add(heroProfile);
                    }
                    else
                    {
                        heroprofiles.Add(heroProfileFromDb);
                    }
                }

                item.Answers = answers;
                item.CategoriesValues = categoriesvalues;
                item.HeroProfiles = heroprofiles;

                dbContext.Categories.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Categories categoryFromDb = await ReadAsync(key, false, false);

                if (categoryFromDb == null)
                {
                    throw new ArgumentException("Category with that Id does not exist!");
                }

                dbContext.Categories.Remove(categoryFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Categories>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Categories> query = dbContext.Categories;

                if (useNavigationalProperties)
                {
                    query = query
                        .Include(c => c.CategoriesValues)
                        .Include(c => c.Answers)
                        .Include(c => c.HeroProfiles);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Categories> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Categories> query = dbContext.Categories;

                if (useNavigationalProperties)
                {
                    query = query
                        .Include(c => c.CategoriesValues)
                        .Include(c => c.Answers)
                        .Include(c => c.HeroProfiles);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(c => c.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Categories item, bool useNavigationalProperties = false)
        {
            try
            {
                Categories categoryFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

                categoryFromDb.Name = item.Name;

                if (useNavigationalProperties)
                {
                    List<Answer> answers = new List<Answer>(item.Answers.Count);
                    object[] keys = new object[3];
                    foreach (Answer answer in item.Answers)
                    {
                        keys = new object[] { answer.Date, answer.CategoryId, answer.GameId };
                        Answer answerFromDb = await dbContext.Answers.FindAsync(keys);

                        if (answerFromDb is null)
                        {
                            answers.Add(answer);
                        }
                        else
                        {
                            answers.Add(answerFromDb);
                        }
                    }

                    List<CategoriesValues> categoriesvalues = new List<CategoriesValues>(item.CategoriesValues.Count);

                    foreach (CategoriesValues category in item.CategoriesValues)
                    {
                        CategoriesValues categoriesValuesFromDb = await dbContext.CategoriesValues.FindAsync(category.Id);

                        if (categoriesValuesFromDb is null)
                        {
                            categoriesvalues.Add(category);
                        }
                        else
                        {
                            categoriesvalues.Add(categoriesValuesFromDb);
                        }
                    }

                    List<HeroProfile> heroprofiles = new List<HeroProfile>(item.HeroProfiles.Count);

                    foreach (HeroProfile heroProfile in item.HeroProfiles)
                    {
                        keys = new object[] { heroProfile.ValueId, heroProfile.GameId, heroProfile.HeroId, heroProfile.CategoryId };
                        HeroProfile heroProfileFromDb = await dbContext.HeroProfiles.FindAsync(keys);

                        if (heroProfileFromDb is null)
                        {
                            heroprofiles.Add(heroProfile);
                        }
                        else
                        {
                            heroprofiles.Add(heroProfileFromDb);
                        }
                    }

                    categoryFromDb.Answers = answers;
                    categoryFromDb.CategoriesValues = categoriesvalues;
                    categoryFromDb.HeroProfiles = heroprofiles;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
