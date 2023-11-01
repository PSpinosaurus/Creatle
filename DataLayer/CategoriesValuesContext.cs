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
                CategoriesValues categoriesvaluesFromDb = await ReadAsync(key);

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

                dbContext.Entry(categoriesvaluesFromDb).CurrentValues.SetValues(item);

                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
