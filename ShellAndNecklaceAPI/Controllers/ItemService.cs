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

        public ItemService(ILogger<ItemService> logger, DbContext newcontext)
        {
            this.logger = logger;
            _context = (OneShotShopContext)newcontext;
        }

        public async Task<IEnumerable<ItemDTO>> GetAll()
        {
            try
            {
                List<ItemDTO> items = new List<ItemDTO>();
                var itemlist = await _context.Items.ToListAsync();

                foreach (Item i in itemlist)
                {
                    var piccomponents = from pics in _context.Pictures
                                    join ftypes in _context.Filetypes on pics.Filetypeid equals ftypes.Id
                                    where pics.Id == i.Pictureid
                                    select new
                                    {
                                        str = pics.Imagename,
                                        strfile = ftypes.Fileextension
                                    };
                    string picstring = piccomponents.ToString();

                    var statusstring = _context.Statuses.SingleAsync(s => s.Id == i.Statusid).ToString();

                    items.Add(new ItemDTO()
                    {
                        Name = i.Itemname,
                        Description = i.Description,
                        PriceBase = (decimal)i.Pricebase,
                        PicString = picstring,
                        Status = statusstring
                    });
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<ItemDTO> Get(string id)
        {
            if(id == null)
            {
                logger.LogError("No ID provided!");
                throw new ArgumentNullException("id");
            }

            Int32 intid = 0;
            if (!Int32.TryParse(id, out intid))
            {
                logger.LogError("ID was not an Integer!");
                {
                    throw new ArgumentException($"Failure: ID could not be converted to type {intid.GetType()}.");
                };
            }

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
            try
            {
                logger.LogInformation("Attempt to delete Item confirmed, but denied. No such action allowed.");
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(DbUpdateException)) throw new Exception("Failed to delete item!");
            }
        }

        public async Task Update(Item freshEntityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
