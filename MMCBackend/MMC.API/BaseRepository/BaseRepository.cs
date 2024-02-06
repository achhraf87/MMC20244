
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MMC.API.Data;

namespace MMC.API.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly AppDbContext _dbContext;
        public BaseRepository(AppDbContext db) {

            this._dbContext = db; 
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var ajt = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return ajt.Entity;

        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            var delete = _dbContext.Set<T>().Remove(entity).Entity;
            await _dbContext.SaveChangesAsync();
            return delete;

        }

        public virtual async Task<List<T>> GetAllAsync()
        {
           return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
