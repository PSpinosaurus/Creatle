using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AnswerContext : IDb<Answer, object[]>
    {
        private readonly CreatleDbContext dbContext;
        public AnswerContext(CreatleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Answer item)
        {
            try
            {
                string date = item.Date.Year + "-" + item.Date.Month + "-" + item.Date.Day;
                item.Date = DateTime.Parse(date);
                CategoriesValues cvFromDb = await dbContext.CategoriesValues.FindAsync(item.CategoryValueId);
                if (cvFromDb != null)
                {
                    item.CategoryValue = cvFromDb;
                }

                dbContext.Answers.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(object[] key)
        {
            try
            {
                Answer answerFromDb = await ReadAsync(key, false, false);

                if (answerFromDb == null)
                {
                    throw new ArgumentException("Answer with that Id does not exist!");
                }

                dbContext.Answers.Remove(answerFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Answer>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Answer> query = dbContext.Answers;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.CategoryValue);
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

        public async Task<Answer> ReadAsync(object[] key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Answer> query = dbContext.Answers;

                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.CategoryValue);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(a => (a.Date == (DateTime)key[0] && a.CategoryId == (int)key[1] && a.GameId == (int)key[2]));
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Answer item, bool useNavigationalProperties = false)
        {
            try
            {
                object[] key = new object[]{item.Date, item.CategoryId,  item.GameId};
                Answer answerFromDb = await ReadAsync(key, useNavigationalProperties, false);
                answerFromDb.CategoryValueId = item.CategoryValueId;

                if (useNavigationalProperties)
                {
                    CategoriesValues cvFromDb = await dbContext.CategoriesValues.FindAsync(item.CategoryValueId);
                    if (cvFromDb != null)
                    {
                        answerFromDb.CategoryValue = cvFromDb;
                    }
                    else
                    {
                        answerFromDb.CategoryValue = item.CategoryValue;
                    }
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
