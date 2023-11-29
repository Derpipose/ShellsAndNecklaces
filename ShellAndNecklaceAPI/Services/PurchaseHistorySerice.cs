using ShellAndNecklaceAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Services
{
    public class PurchaseHistorySerice
    {
        private readonly ILogger<PurchaseHistorySerice> logger;
        private OneShotShopContext _Context;

        public PurchaseHistorySerice(ILogger<PurchaseHistorySerice> logger)
        {
            this.logger = logger;
        }

        public async Task<List<OrderDTO>> GetPurchaseHistory(Account acc)
        {
            try
            {
                logger.LogInformation($"Attempting to get purchase history for {acc.Username}");

                List<Purchaseorder> PurchaseHistory = (List<Purchaseorder>)from p in _Context.Purchaseorders
                                      join o in _Context.Orderitems on p.Id equals o.Orderid
                                      join a in _Context.Accounts on p.Accountid equals a.Id
                                      where a.Username == acc.Username && a.Password == acc.Password
                                      select p;

                var itemsordered = from o in _Context.Orderitems
                                              join p in _Context.Purchaseorders on o.Orderid equals p.Id
                                              join i in _Context.Items on o.Itemid equals i.Id
                                              join pc in _Context.Pictures on i.Pictureid equals pc.Id
                                              join f in _Context.Filetypes on pc.Filetypeid equals f.Id
                                              where p.Accountid == acc.Id
                                              select new
                                              {
                                                  orderid = o.Orderid,
                                                  name = i.Itemname,
                                                  desc = i.Description,
                                                  picstring = pc.Imagename + f.Fileextension,
                                                  quant = o.Quantity,
                                                  price = o.Pricepaid
                                              };

                var UserPurchaseHistory = new List<OrderDTO>();

                foreach (Purchaseorder ph in PurchaseHistory){
                    List<PurchasedItemDTO> ordered = new List<PurchasedItemDTO>();
                    decimal accumulator = 0;
                    
                    foreach(var i in itemsordered)
                    {
                        if(i.orderid == ph.Id)
                            ordered.Add(new PurchasedItemDTO(){
                                Name = i.name,
                                Description = i.desc,
                                PictureString = i.picstring,
                                Quantity = i.quant,
                                Price = i.price,
                            });
                        accumulator += i.price;
                    }
                    UserPurchaseHistory.Add(new OrderDTO()
                    {
                        OrderDate = (DateTime)ph.Orderdate,
                        Notes = ph.Notes,
                        Items = ordered,
                        OrderTotal = accumulator
                    });                    
                }

                return UserPurchaseHistory;
            }
            catch(Exception ex)
            {
                throw new BadInputException("Account history failure");
            }
        }

        public async Task CreatePurchase()
        {
            throw new NotImplementedException();
        }
    }
}
