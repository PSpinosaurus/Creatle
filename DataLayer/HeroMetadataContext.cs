using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class HeroMetadataContext : IDb<HeroMetadata, int>
    {
        private readonly CreatleDbContext dbContext;
        public HeroMetadataContext(CreatleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(HeroMetadata item)
        {
            try
            {
                object[] keys = new object[4];
                List<HeroProfile> heroprofiles = new List<HeroProfile>(item.HeroProfiles.Count);

                foreach (HeroProfile heroProfile in item.HeroProfiles)
                {
                    keys = new object[] { heroProfile.ValueId, heroProfile.GameId, heroProfile.HeroId, heroProfile.CategoryId };
                    HeroProfile heroprofileFromDb = await dbContext.HeroProfiles.FindAsync(keys);

                    if (heroprofileFromDb is null)
                    {
                        heroprofiles.Add(heroProfile);
                    }
                    else
                    {
                        heroprofiles.Add(heroprofileFromDb);
                    }
                }

                item.HeroProfiles = heroprofiles;

                dbContext.HeroMetadata.Add(item);
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
                HeroMetadata herometadataFromDb = await ReadAsync(key, false, false);

                if (herometadataFromDb == null)
                {
                    throw new ArgumentException("HeroMetadata with that Id does not exist!");
                }

                dbContext.HeroMetadata.Remove(herometadataFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<HeroMetadata>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<HeroMetadata> query = dbContext.HeroMetadata;

                if (useNavigationalProperties)
                {
                    query = query.Include(h => h.HeroProfiles);
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

        public async Task<HeroMetadata> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<HeroMetadata> query = dbContext.HeroMetadata;

                if (useNavigationalProperties)
                {
                    query = query.Include(h => h.HeroProfiles);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(h => h.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(HeroMetadata item, bool useNavigationalProperties = false)
        {
            try
            {
                HeroMetadata herometadataFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

                dbContext.Entry(herometadataFromDb).CurrentValues.SetValues(item);

                if (useNavigationalProperties)
                {
                    object[] keys = new object[4];
                    List<HeroProfile> heroprofiles = new List<HeroProfile>(item.HeroProfiles.Count);

                    foreach (HeroProfile heroProfile in item.HeroProfiles)
                    {
                        keys = new object[] { heroProfile.ValueId, heroProfile.GameId, heroProfile.HeroId, heroProfile.CategoryId };
                        HeroProfile heroprofileFromDb = await dbContext.HeroProfiles.FindAsync(keys);

                        if (heroprofileFromDb is null)
                        {
                            heroprofiles.Add(heroProfile);
                        }
                        else
                        {
                            heroprofiles.Add(heroprofileFromDb);
                        }
                    }

                    herometadataFromDb.HeroProfiles = heroprofiles;
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
