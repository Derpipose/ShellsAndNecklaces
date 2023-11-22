using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Controllers
{
    public class AccountService
    {
        private readonly ILogger<AccountService> logger;
        private OneShotShopContext _context;

        public AccountService(ILogger<AccountService> logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<AccountDTO>> GetAll()
        {
            logger.LogInformation("Confirmed; attempt to get all account details recognized.\n" +
                "Access denied: no such action permitted!");
            throw new NotImplementedException();

            var AccList = _context.Accounts.ToListAsync();
            List<AccountDTO> Acc = new List<AccountDTO>();

            foreach (var account in Acc)
            {
                Acc.Add(new AccountDTO
                {
                    Username = account.Username,
                    Email = account.Email,
                    Address = account.Address,
                    Phone = account.Phone,
                    Verified = account.Verified,
                    Closed = account.Closed
                });
            }

            return Acc;
        }

        [HttpGet("account")]
        public async Task<AccountDTO> Get(string user)
        {
            logger.LogInformation("Account details access attempted at " + DateTime.Now);
            var context = await _context.Accounts.SingleAsync(acc => (acc.Username == user));
            if(context == null)
            {
                throw new ArgumentNullException("User not found!");
            }

            logger.LogInformation("\nAccount " + context.Username + " accessed.");

            if (!(bool)context.Verified)
            {
                logger.LogCritical("Accessed account has already been closed!");
                throw new Exception("This account was closed.");
            }

            return new AccountDTO
            {
                Username = context.Username,
                Phone = context.Phone,
                Email = context.Email,
                Address = context.Address,
                Verified = context.Verified
            };
        }

        [HttpPost("update")]
        public async Task<AccountDTO> Update(Account account)
        {
            logger.LogInformation($"Account detail attempt for account {account.Username} at {DateTime.Now}.");
            try
            {
                var updateparcel = await _context.Accounts.SingleAsync(a => a.Id ==  account.Id);
                var updatedparcel = new AccountDTO
                {
                    Username = account.Username,
                    Phone = account.Phone,
                    Email = account.Email,
                    Address = account.Address,
                    Verified = account.Verified,
                };
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
                return updatedparcel;
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to update Account Information!");
                throw new Exception("Failed to update Account Information!");
            }   
        }

        [HttpDelete("close")]
        public async Task Delete(string user, string pass)
        {
            logger.LogInformation($"Attempt to close account {user} confirmed. Proceeding...");

            try
            {
                var usercontext = await _context.Accounts.SingleAsync(a => (a.Username == user && a.Password == pass));
                if(usercontext == null)
                {
                    throw new ArgumentNullException("User not found!");
                }
                
                await _context.SaveChangesAsync();
                logger.LogInformation("Success!");
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                throw new Exception("Failed to update account!");
            }
        }
    }
}
