using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace ShellAndNecklaceAPI.Services
{
    public class CartService
    {
        private readonly ILogger<CartService> logger;
        private OneShotShopContext _context;

        public CartService(ILogger<CartService> logger, OneShotShopContext context)
        {
            this.logger = logger;
            _context = context;
        }

        public async Task<List<CartDTO>> GetUserCart(string user)
        {
            try
            {
                logger.LogInformation("Attempting cart access...");
                var cartlist = (List<Cart>)from c in _context.Carts
                                           join a in _context.Accounts on c.Accountid equals a.Id
                                           where a.Email.Contains(user)
                                           select c;

                if (cartlist == null)
                    throw new ArgumentNullException("No cart items found!");

                var itemlist = await _context.Items.ToListAsync();

                if (itemlist == null)
                    throw new ArgumentNullException("Failure to retrieve item list!");
                
                var userspecificcartlist = new List<CartDTO>();
                foreach (var cartitem in cartlist) {
                    userspecificcartlist.Add(new CartDTO
                    {
                        id = cartitem.Id,
                        itemname = itemlist.Find(x => x.Id == cartitem.Id).Itemname,
                        quantity = (int)cartitem.Quantity,
                        actualprice = (decimal)cartitem.Actualprice
                    });
                }

                logger.LogInformation("Cart data found. Returning...");

                return userspecificcartlist;
            }
            catch(Exception ex)
            {
                logger.LogError($"Failure {ex.Message} occurred at {DateTime.Now}");
                switch(ex.Message)
                {
                    case "No cart items found!": throw new ArgumentNullException();
                        break;
                    case "Failure to retrieve item list!": throw new KeyNotFoundException();
                        break;
                    default: throw new Exception("An unhandled exception occurred.");
                        break;
                }
            }
        }

        public async Task CompleteTransaction(string user, string notes)
        {
            try
            {
                logger.LogInformation($"Completing transaction for account {user}...");
                var accid = (await _context.Accounts.FirstOrDefaultAsync(a => a.Email == user)).Id;
                List<Cart> cartItems = await _context.Carts.Where(c => c.Accountid == accid).ToListAsync();

                logger.LogInformation($"Purchase date: {DateTime.Now}");
                _context.Purchaseorders.Add(new Purchaseorder()
                {
                    Accountid = accid,
                    Notes = notes,
                    Orderdate = DateTime.Now
                });
                await _context.SaveChangesAsync();
                var latestPOId = _context.Purchaseorders.MaxBy(po => po.Orderdate).Id;

                logger.LogInformation("Adding to Orderitems");
                foreach (var c in cartItems)
                {
                    _context.Orderitems.Add(new Orderitem()
                    {
                        Itemid = c.Itemid,
                        Pricepaid = (decimal)c.Actualprice,
                        Quantity = (int)c.Quantity,
                        Orderid = latestPOId
                    });
                }

                await _context.SaveChangesAsync();

                this.DeleteCart(user);
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}");
                throw new Exception();
            }
            
        }

        public async Task AddToCart(CartDTO cart)
        {
            try
            {
                logger.LogInformation($"Attempting to add item {cart.itemname} to cart...");
                var itemid = (await _context.Items.FirstOrDefaultAsync(i => i.Itemname == cart.itemname)).Id;

                Account accid = null;
                try
                {
                    accid = (await _context.Accounts.FirstOrDefaultAsync(a => a.Email == cart.email));
                    
                    if (accid == null)
                    {
                        accid = new Account()
                        {
                            Email = cart.email
                        };
                        _context.Accounts.Add(accid);

                        await _context.SaveChangesAsync();
                        accid = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == cart.email);
                    }
                }
                catch
                {
                    if (accid == null)
                    {
                        accid = new Account()
                        {
                            Email = cart.email
                        };
                        _context.Accounts.Add(accid);

                        await _context.SaveChangesAsync();
                        accid = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == cart.email);
                    }
                }

                if(itemid == null)
                    throw new KeyNotFoundException($"Item {cart.itemname} not found!");

                _context.Carts.Add(new Cart()
                {
                    Itemid = itemid,
                    Accountid = accid.Id,
                    Actualprice = cart.actualprice,
                    Quantity = cart.quantity,
                });
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError(ex.Message);
                throw new Exception();
            }
            catch (Exception ex)
            {
                logger.LogError($"Unexpected failure: {ex.Message}\nTime: {DateTime.Now}");
                throw new Exception();
            }
        }

        public async Task DeleteCart(string user)
        {
            logger.LogInformation("Clearing cart for user.");
            
            try
            {
                var accid = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == user);
                _context.Carts.RemoveRange(
                    _context.Carts.Where(c => c.Accountid == accid.Id));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                logger.LogError("Something went wrong when trying to clear the user's cart.");
            }
        }

        public async Task RemoveFromCart(int id)
        {
            var removed = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (removed != null) { throw new KeyNotFoundException(); }
            _context.Carts.Remove(removed);
            await _context.SaveChangesAsync();
        }
    }
}
