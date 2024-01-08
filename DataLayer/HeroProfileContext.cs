using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class HeroProfileContext : IDb<HeroProfile, object>
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
                dbContext.HeroProfiles.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(object key)
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

        public async Task<HeroProfile> ReadAsync(object key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<HeroProfile> query = dbContext.HeroProfiles;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(h => new { h.GameId, h.CategoryId, h.HeroId, h.ValueId } == key);
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
                HeroProfile heroprofileFromDb = await ReadAsync(item.GameId, useNavigationalProperties, false);

                dbContext.Entry(heroprofileFromDb).CurrentValues.SetValues(item);

                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
