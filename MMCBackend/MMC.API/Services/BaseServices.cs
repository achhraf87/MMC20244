
using MMC.API.BaseRepository;
using MMC.API.Repository;

namespace MMC.API.Services
{
    public class BaseServices<T> : IBaseRepository<T> where T : class
    {
        protected IBaseRepository<T> _repository;
        public BaseServices(IBaseRepository<T> repository)
        {
           _repository = repository;
        }

        public Task<T> CreateAsync(T entity)
        {
           return _repository.CreateAsync(entity);
        }

        public Task<T> DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<T> GetByIdAsync(int Id)
        {
            return _repository.GetByIdAsync(Id);
        }

        public Task<T> UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }
    }
}
