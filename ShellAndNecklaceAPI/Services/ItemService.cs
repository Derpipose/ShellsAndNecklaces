using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Services;
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
                logger.LogInformation("Retrieving item list...");
                List<ItemDTO> items = new List<ItemDTO>();
                var itemlist = await _context.Items.ToListAsync();
                var statusstringlist = await _context.Statuses.ToListAsync();

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
                    string statusstring = "";
                    foreach (Status s in statusstringlist)
                    {
                        if (i.Statusid == s.Id)
                        {
                            statusstring = s.Status1;
                        }
                    }

                    items.Add(new ItemDTO()
                    {
                        Name = i.Itemname,
                        Description = i.Description,
                        PriceBase = (decimal)i.Pricebase,
                        PicId = i.Pictureid ?? 33,
                        StatusId = i.Statusid ?? 9
                    });
                }
                logger.LogInformation("Item list compiled as data transfer object.\nReturning...");
                return items;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failure {ex.Message} occurred in item list retrieval!");
                throw new Exception("Failure in retrieving items!");
            }
        }

        public async Task<ItemDTO> Get(string name)
        {
            if (name == null)
            {
                logger.LogError("No ID provided!");
                throw new ArgumentNullException("user");
            }
            try
            {
                logger.LogInformation("Item name provided, attempting access.");
                ItemDTO itemEnt = (ItemDTO)(from i in _context.Items
                                            join p in _context.Pictures
                                              on i.Pictureid equals p.Id
                                            join f in _context.Filetypes
                                              on p.Filetypeid equals f.Id
                                            join s in _context.Statuses
                                              on i.Statusid equals s.Id
                                            select new
                                            {
                                                Name = i.Itemname,
                                                Description = i.Description,
                                                Status = s.Status1,
                                                PriceBase = i.Pricebase,
                                                PicString = p.Imagename.Concat(f.Fileextension)
                                            });
                if (itemEnt.Name == null)
                {
                    throw new Exception("item not found");
                }

                logger.LogInformation("Access successful, returning found item.");
                return itemEnt;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failure \"{ex.Message}\" occurred in item retrieval at " + DateTime.Now + "!");
                throw new KeyNotFoundException(name + ", " + ex.Message);
            }
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
