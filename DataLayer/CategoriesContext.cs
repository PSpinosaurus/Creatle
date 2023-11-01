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
                dbContext.Categories.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Categories categoryFromDb = await ReadAsync(key);

                if (categoryFromDb == null)
                {
                    throw new ArgumentException("Author with that Id does not exist!");
                }

                dbContext.Categories.Remove(categoryFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
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
            catch(Exception)
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
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Categories item, bool useNavigationalProperties = false)
        {
            try
            {
                Categories categoryFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

                dbContext.Entry(categoryFromDb).CurrentValues.SetValues(item);

                if (useNavigationalProperties)
                {
                    List<Answer> answers = new List<Answer>(item.Answers.Count);

                    foreach(Answer answer in item.Answers)
                    {
                        Answer answerFromDb = await dbContext.Answers.FindAsync(answer.GameId, answer.CategoryId, answer.Date);

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

                    foreach(CategoriesValues category in item.CategoriesValues)
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

                    foreach(HeroProfile heroProfile in item.HeroProfiles)
                    {
                        HeroProfile heroProfileFromDb = await dbContext.HeroProfiles.FindAsync(heroProfile.GameId, heroProfile.ValueId, heroProfile.HeroId, heroProfile.CategoryId);

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
            catch(Exception)
            {
                throw;
            }
        }
    }
}
