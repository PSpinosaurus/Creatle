using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class GameManager
    {
        private readonly GameContext gameContext;

        public GameManager(GameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        public async Task CreateAsync(Game item)
        {
            await gameContext.CreateAsync(item);
        }

        public async Task<Game> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await gameContext.ReadAsync(key, useNavigationalProperties, isReadOnly);
        }

        public async Task<ICollection<Game>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await gameContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(Game item, bool useNavigationalProperties = false)
        {
            await gameContext.UpdateAsync(item, useNavigationalProperties);
        }

        public async Task DeleteAsync(int key)
        {
            await gameContext.DeleteAsync(key);
        }
    }
}
