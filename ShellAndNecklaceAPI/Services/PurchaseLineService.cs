using ShellAndNecklaceAPI.Controllers;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Services
{
    public class PurchaseLineService
    {
        private readonly ILogger<PurchaseLineService> logger;
        private OneShotShopContext DbContext;

        public PurchaseLineService(ILogger<PurchaseLineService> logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<PurchaseLineDTO>> GetAll()
        {
            throw new NotImplementedException(); 
        }


        Task<PurchaseLineService> Get(string id)
        {
            throw new NotImplementedException();
        }

        Task Update(PurchaseLineService freshentity)
        {
            throw new NotImplementedException();
        }

        Task Delete(PurchaseLineService freshentity)
        {
            throw new NotImplementedException();
        }

        //public async Task<PurchaseLineDTO>
    }
}
