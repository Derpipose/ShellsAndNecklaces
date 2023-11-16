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
        private OneShotShopContext DbContext;

        public AccountService(ILogger<AccountService> logger)
        {
            this.logger = logger;
        }

        public async Task<AccountDTO> GetAccountDetails(string user)
        {
            logger.LogInformation("Account details access attempted at " + DateTime.Now);
            var context = await DbContext.Accounts.SingleAsync(acc => (acc.Username == user));
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

        public async Task UpdateAccount(AccountDTO joke)
        {

        }

        public async Task CloseAccount(string user, string pass)
        {
            logger.LogInformation("Attempt to close account confirmed. Proceeding...");

            var usercontext = await DbContext.Accounts.SingleAsync(a => (a.Username == user));
            if(usercontext == null)
                        {
                throw new ArgumentNullException("User not found!");
            }

            try
            {
                DbContext.Accounts.Remove(usercontext);
                await DbContext.SaveChangesAsync();
                logger.LogInformation("Success!");
            }
            catch (Exception ex)
            {

            }

        }
    }
}
