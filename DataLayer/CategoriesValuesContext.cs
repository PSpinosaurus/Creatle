using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoriesValuesContext : IDb<CategoriesValues, int>
    {
        private readonly CreatleDbContext dbContext;
        public CategoriesValuesContext(CreatleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CategoriesValues item)
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

                Categories categoryFromDb = await dbContext.Categories.FindAsync(item.CategoryId);
                if (categoryFromDb != null)
                {
                    item.Category = categoryFromDb;
                }

                item.HeroProfiles = heroprofiles;
                item.Answers = answers;

                dbContext.CategoriesValues.Add(item);
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
                CategoriesValues categoriesvaluesFromDb = await ReadAsync(key, false, false);

                if (categoriesvaluesFromDb == null)
                {
                    throw new ArgumentException("CategoriesValues with that Id does not exist!");
                }

                dbContext.CategoriesValues.Remove(categoriesvaluesFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<CategoriesValues>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<CategoriesValues> query = dbContext.CategoriesValues;

                if (useNavigationalProperties)
                {
                    query.Include(cv => cv.Category)
                         .Include(cv => cv.HeroProfiles)
                         .Include(cv => cv.Answers);
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

        public async Task<CategoriesValues> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<CategoriesValues> query = dbContext.CategoriesValues;
                if (useNavigationalProperties)
                {
                    query.Include(cv => cv.Category)
                         .Include(cv => cv.HeroProfiles)
                         .Include(cv => cv.Answers);
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

        public async Task UpdateAsync(CategoriesValues item, bool useNavigationalProperties = false)
        {
            try
            {
                CategoriesValues categoriesvaluesFromDb = await ReadAsync(item.Id, false, false);

                categoriesvaluesFromDb.Value = item.Value;

                if (useNavigationalProperties)
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

                    Categories categoryFromDb = await dbContext.Categories.FindAsync(item.CategoryId);
                    if (categoryFromDb != null)
                    {
                        categoriesvaluesFromDb.Category = categoryFromDb;
                    }
                    else
                    {
                        categoriesvaluesFromDb.Category = item.Category;
                    }

                    categoriesvaluesFromDb.HeroProfiles = heroprofiles;
                    categoriesvaluesFromDb.Answers = answers;
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
