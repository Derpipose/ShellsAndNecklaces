using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;
using ShellAndNecklaceAPI.Services;

namespace ShellAndNecklaceAPI.Controllers
{
    public class ItemService
    {
        private readonly ILogger<ItemService> logger;
        private OneShotShopContext _context;

        public ItemService(ILogger<ItemService> logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<ItemDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ItemDTO> Get(string id)
        {
            if(id == null)
                throw new ArgumentNullException("id");

            int intid = 0;
            if (!Int32.TryParse(id, out intid))
                throw new ArgumentException("id is bad");

            ItemDTO itemEnt = (ItemDTO)(from i in _context.Items
                          join p in _context.Pictures
                            on i.Pictureid equals p.Id
                          join f in _context.Filetypes
                            on p.Filetypeid equals f.Id
                          select new
                          {
                              Name = i.Itemname,
                              Description = i.Description,
                              Status = i.Status,
                              PriceBase = i.Pricebase,
                              PicString = p.Imagename.Concat(f.Fileextension)
                          });

            return itemEnt;
        }

        public async Task Delete(Item itemEntityToDelete)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Item freshEntityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
