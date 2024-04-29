using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class HeroProfileContext : IDb<HeroProfile, object[]>
    {
        private readonly CreatleDbContext dbContext;
        public HeroProfileContext(CreatleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(HeroProfile item)
        {
            try
            {
                CategoriesValues cvFromDb = await dbContext.CategoriesValues.FindAsync(item.ValueId);
                if (cvFromDb != null)
                {
                    item.Value = cvFromDb;
                }

                dbContext.HeroProfiles.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(object[] key)
        {
            try
            {
                HeroProfile heroprofileFromDb = await ReadAsync(key, false, false);

                if (heroprofileFromDb == null)
                {
                    throw new ArgumentException("HeroProfile with that Id does not exist!");
                }

                dbContext.HeroProfiles.Remove(heroprofileFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<HeroProfile>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<HeroProfile> query = dbContext.HeroProfiles;

                if (useNavigationalProperties)
                {
                    query = query.Include(h => h.Value);
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

        public async Task<HeroProfile> ReadAsync(object[] key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<HeroProfile> query = dbContext.HeroProfiles;

                if (useNavigationalProperties)
                {
                    query = query.Include(h => h.Value);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(h => (h.ValueId == (int)key[0] && 
                                                             h.GameId == (int)key[1] && 
                                                             h.HeroId == (int)key[2] && 
                                                             h.CategoryId == (int)key[3]));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(HeroProfile item, bool useNavigationalProperties = false)
        {
            try
            {
                object[] key = new object[] { item.ValueId, item.GameId, item.HeroId, item.CategoryId };
                HeroProfile heroprofileFromDb = await ReadAsync(key, useNavigationalProperties, false);

                if (useNavigationalProperties)
                {
                    CategoriesValues cvFromDb = await dbContext.CategoriesValues.FindAsync(item.ValueId);
                    if (cvFromDb != null)
                    {
                        heroprofileFromDb.Value = cvFromDb;
                    }
                    else
                    {
                        heroprofileFromDb.Value = item.Value;
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
