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
    public class AnswerContext : IDb<Answer, object>
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
                dbContext.Answers.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(object key)
        {
            try
            {
                Answer answerFromDb = await ReadAsync(key);

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

        public async Task<Answer> ReadAsync(object key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Answer> query = dbContext.Answers;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(a => new { a.Date, a.GameId, a.CategoryId } == key);
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
                Answer answerFromDb = await ReadAsync(item.GameId, false, false);
                dbContext.Entry(answerFromDb).CurrentValues.SetValues(item);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
