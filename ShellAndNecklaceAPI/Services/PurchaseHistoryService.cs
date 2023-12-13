using ShellAndNecklaceAPI.Services;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Services;
public class PurchaseHistoryService
{
    private readonly ILogger<PurchaseHistoryService> logger;
    private OneShotShopContext _Context;

    public PurchaseHistoryService(ILogger<PurchaseHistoryService> logger, OneShotShopContext context)
    {
        this.logger = logger;
        _Context = context;
    }

    public async Task<List<OrderDTO>> GetPurchaseHistory(AccountDTO acc)
    {
        try
        {
            logger.LogInformation($"Attempting to get purchase history for {acc.Username}");

            List<Purchaseorder> PurchaseHistory = (List<Purchaseorder>)(from p in _Context.Purchaseorders
                                                                           join o in _Context.Orderitems on p.Id equals o.Orderid
                                                                           join a in _Context.Accounts on p.Accountid equals a.Id
                                                                           where a.Username == acc.Username
                                                                           select p);

            var itemsordered = from o in _Context.Orderitems
                               join p in _Context.Purchaseorders on o.Orderid equals p.Id
                               join i in _Context.Items on o.Itemid equals i.Id
                               join pc in _Context.Pictures on i.Pictureid equals pc.Id
                               join f in _Context.Filetypes on pc.Filetypeid equals f.Id
                               join a in _Context.Accounts on p.Accountid equals a.Id
                               where a.Email == acc.Username && p.Accountid == a.Id
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

            foreach (Purchaseorder ph in PurchaseHistory)
            {
                List<PurchasedItemDTO> ordered = new List<PurchasedItemDTO>();
                decimal accumulator = 0;

                foreach (var i in itemsordered)
                {
                    if (i.orderid == ph.Id)
                        ordered.Add(new PurchasedItemDTO()
                        {
                            Name = i.name,
                            Notes = i.desc,
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
        catch (Exception ex)
        {
            throw new BadInputException("Account history failure");
        }
    }

    public async Task CreatePurchase(OrderDTO newPurchase)
    {
        decimal ordertotal = 0;
        foreach (PurchasedItemDTO pitem in newPurchase.Items)
        {
            ordertotal += pitem.Price;
        }
        var accid = await _Context.Accounts.SingleAsync(a => a.Email == newPurchase.Email);

        var Purchase = new Purchaseorder()
        {
            Accountid = accid.Id,
            Orderdate = newPurchase.OrderDate,
            Notes = newPurchase.Notes
        };

        _Context.Add(Purchase);
        await _Context.SaveChangesAsync();

        var purchid = _Context.Purchaseorders.MaxBy(po => po.Orderdate).Id;

        foreach (var pitem in newPurchase.Items)
        {
            _Context.Add(new Orderitem()
            {
                Orderid = purchid,
                Pricepaid = pitem.Price,
                Quantity = pitem.Quantity,
                Itemid = _Context.Items.SingleAsync(op => op.Itemname == pitem.Name).Id
            });
        }

        await _Context.SaveChangesAsync();
    }
}
