using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class AnswerManager
    {
        private readonly AnswerContext answerContext;

        public AnswerManager(AnswerContext answerContext)
        {
            this.answerContext = answerContext;
        }

        public async Task CreateAsync(Answer item)
        {
            await answerContext.CreateAsync(item);
        }

        public async Task<Answer> ReadAsync(dynamic key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await answerContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<Answer>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await answerContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Answer item, bool useNavigationalProperties = false)
        {
            await answerContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(dynamic key)
        {
            await answerContext.DeleteAsync(key);
        }
    }
}