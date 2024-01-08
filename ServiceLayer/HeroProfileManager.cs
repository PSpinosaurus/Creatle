using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class HeroProfileManager
    {
        private readonly HeroProfileContext heroProfileContext;

        public HeroProfileManager(HeroProfileContext heroProfileContext)
        {
            this.heroProfileContext = heroProfileContext;
        }

        public async Task CreateAsync(HeroProfile item)
        {
            await heroProfileContext.CreateAsync(item);
        }

        public async Task<HeroProfile> ReadAsync(dynamic key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await heroProfileContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<HeroProfile>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await heroProfileContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(HeroProfile item, bool useNavigationalProperties = false)
        {
            await heroProfileContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(dynamic key)
        {
            await heroProfileContext.DeleteAsync(key);
        }
    }
}
