using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class HeroMetadataManager
    {
        private readonly HeroMetadataContext heroMetadataContext;

        public HeroMetadataManager(HeroMetadataContext heroMetadataContext)
        {
            this.heroMetadataContext = heroMetadataContext;
        }

        public async Task CreateAsync(HeroMetadata item)
        {
            await heroMetadataContext.CreateAsync(item);
        }

        public async Task<HeroMetadata> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await heroMetadataContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<HeroMetadata>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await heroMetadataContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(HeroMetadata item, bool useNavigationalProperties = false)
        {
            await heroMetadataContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await heroMetadataContext.DeleteAsync(key);
        }
    }
}
