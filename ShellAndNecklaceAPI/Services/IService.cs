namespace ShellAndNecklaceAPI.Services
{
    public interface IService<T>
    {
        public Task Create(T newentity);
        public Task Get(string id);
        public Task<T> Update(T freshentity);
        public Task Delete(T freshentity);
    }
}
