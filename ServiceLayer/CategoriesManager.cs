using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CategoriesManager
    {
        private readonly CategoriesContext categoriesContext;

        public CategoriesManager(CategoriesContext categoriesContext)
        {
            this.categoriesContext = categoriesContext;
        }

        public async Task CreateAsync(Categories item)
        {
            await categoriesContext.CreateAsync(item);
        }

        public async Task<Categories> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await categoriesContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<Categories>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await categoriesContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Categories item, bool useNavigationalProperties = false)
        {
            await categoriesContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await categoriesContext.DeleteAsync(key);
        }
    }

}
