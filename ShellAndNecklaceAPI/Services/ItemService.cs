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

        public async Task<List<ItemDTO>> GetAll()
        {
            try
            {
                logger.LogInformation("Retrieving item list...");
                List<ItemDTO> items = new List<ItemDTO>();
                var itemlist = await _context.Items.ToListAsync();
                var statusstringlist = await _context.Statuses.ToListAsync();

                foreach (Item i in itemlist)
                {
                    var piccomponents = await _context.Pictures.FirstOrDefaultAsync(p => p.Id == i.Pictureid);
                    var filecomponents = await _context.Filetypes.FirstOrDefaultAsync(f => f.Id == piccomponents.Filetypeid);
                    string picstring = piccomponents.Imagename + filecomponents.Fileextension;
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
                        PicString = picstring,
                        StatusType = statusstring
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
            var itemdeets = await _context.Items.Where(i => i.Itemname == name).FirstOrDefaultAsync();
            var piccontents = await _context.Pictures.Where(p => p.Id == itemdeets.Pictureid).FirstOrDefaultAsync();
            var filedeets = await _context.Filetypes.Where(ft => ft.Id == piccontents.Filetypeid).FirstOrDefaultAsync();
                var statusdeets = await _context.Statuses.Where(s => s.Id == itemdeets.Statusid).FirstOrDefaultAsync();

                ItemDTO itemEnt = new ItemDTO() {
                    Name = itemdeets.Itemname,
                    Description = itemdeets.Description,
                    PicString = piccontents.Imagename + filedeets.Fileextension,
                    PriceBase = itemdeets.Pricebase,
                    StatusType = statusdeets.Status1
                };
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
