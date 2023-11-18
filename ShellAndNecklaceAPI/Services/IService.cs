namespace ShellAndNecklaceAPI.Services
{
    public interface IService<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(string id);
        public Task Update(T freshentity);
        public Task Delete(T freshentity);
    }
}
