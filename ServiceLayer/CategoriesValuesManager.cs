using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CategoriesValuesManager
    {
        private readonly CategoriesValuesContext categoriesValuesContext;

        public CategoriesValuesManager(CategoriesValuesContext categoriesValuesContext)
        {
            this.categoriesValuesContext = categoriesValuesContext;
        }

        public async Task CreateAsync(CategoriesValues item)
        {
            await categoriesValuesContext.CreateAsync(item);
        }

        public async Task<CategoriesValues> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await categoriesValuesContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<CategoriesValues>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await categoriesValuesContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(CategoriesValues item, bool useNavigationalProperties = false)
        {
            await categoriesValuesContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await categoriesValuesContext.DeleteAsync(key);
        }
    }
}
