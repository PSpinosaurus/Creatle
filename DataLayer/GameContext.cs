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
    public class GameContext : IDb<Game, int>
    {
        private readonly CreatleDbContext dbContext;
        public GameContext(CreatleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(Game item)
        {
            try
            {               
                dbContext.Games.Add(item);
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
                Game gameFromDb = await ReadAsync(key, false, false);

                if (gameFromDb == null)
                {
                    throw new ArgumentException("Game with that Id does not exist!");
                }

                dbContext.Games.Remove(gameFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Game>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Game> query = dbContext.Games;

                if (useNavigationalProperties)
                {
                    query = query
                        .Include(g => g.HeroesProfiles)
                        .Include(g => g.Answers);
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

        public async Task<Game> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Game> query = dbContext.Games;

                if (useNavigationalProperties)
                {
                    query = query
                        .Include(g => g.HeroesProfiles)
                        .Include(g => g.Answers);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(g => g.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Game item, bool useNavigationalProperties = false)
        {
            try
            {
                Game gameFromDb = await ReadAsync(item.Id, useNavigationalProperties, false);

                gameFromDb.Description = item.Description;
                gameFromDb.Name = item.Name;
                gameFromDb.Colour = item.Colour;

                if (useNavigationalProperties)
                {
                    object[] keys = new object[4];
                    List<HeroProfile> heroprofiles = new List<HeroProfile>(item.HeroesProfiles.Count);

                    foreach(HeroProfile heroProfile in item.HeroesProfiles)
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

                    List<Answer> answers = new List<Answer>(item.Answers.Count);

                    foreach (Answer answer in item.Answers)
                    {
                        keys = new object[] { answer.Date, answer.CategoryId, answer.GameId };
                        Answer answerFromDb = await dbContext.Answers.FindAsync(keys);

                        if (answerFromDb is null)
                        {
                            answers.Add(answer);
                        }
                        else
                        {
                            answers.Add(answerFromDb);
                        }
                    }

                    gameFromDb.HeroesProfiles = heroprofiles;
                    gameFromDb.Answers = answers;
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
