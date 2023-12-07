using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Services;

    public class AccountService
    {
        private readonly ILogger<AccountService> logger;
        private OneShotShopContext _context;

        public AccountService(ILogger<AccountService> logger)
        {
            this.logger = logger;
        }

        public async Task CreateAccount(AccountDTO acc)
        {
            try
            {
                if(acc == null || acc.Email == null)
                {
                    throw new ArgumentNullException("No account provided.");
                }    

                logger.LogInformation("Account received. Attempting to create entity...");
                Account newAcc = new Account()
                {
                    Address = acc.Address,
                    Email = acc.Email,
                    Username = acc.Username,
                    Verified = true,
                    Phone = acc.Phone
                };
                
                _context.Accounts.Add(newAcc);
            }
            catch (Exception ex) {
                logger.LogError($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<AccountDTO>> GetAll()
        {
            logger.LogInformation("confirmed; attempt to get all account details recognized.\n" +
                "access denied: no such action permitted!");
            //throw new notimplementedexception();

            var acclist = await _context.Accounts.ToListAsync();
            List<AccountDTO> acc = new List<AccountDTO>();

            foreach (var account in acclist)
            {
                acc.Add(new AccountDTO
                {
                    Username = account.Username,
                    Email = account.Email,
                    Address = account.Address,
                    Phone = account.Phone,
                    Verified = account.Verified,
                    Closed = account.Closed
                });
            }

            return acc;
        }
        
        public async Task<bool> AccountExists(string user)
        {
            var accstatus = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == user);

            if (accstatus == null) return false;
            else return true;
        }
        public async Task<AccountDTO> Get(string user)
        {
            logger.LogInformation("account details access attempted at " + DateTime.Now);
            var context = await _context.Accounts.SingleAsync(acc => (acc.Email == user));
            if (context == null)
            {
                throw new ArgumentNullException("user not found!");
            }

            if ((bool)context.Closed)
            {
                throw new UnauthorizedAccessException("account is closed!");
            }

            logger.LogInformation("\naccount " + context.Email + " accessed.");

            if (!(bool)context.Verified)
            {
                logger.LogCritical("accessed account has already been closed!");
                throw new Exception("this account was closed.");
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

        public async Task<AccountDTO> Update(Account account)
        {
            logger.LogInformation($"account detail attempt for account {account.Email} at {DateTime.Now}.");
            try
            {
                var updateparcel = await _context.Accounts.SingleAsync(a => a.Email == account.Email);

                if ((bool)updateparcel.Closed || updateparcel == null)
                    throw new Exception();

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
                logger.LogError("failed to update account information!");
                throw new Exception("failed to update account information!");
            }
        }

        public async Task Delete(string user)
        {
            logger.LogInformation($"attempt to close account {user} confirmed. proceeding...");

            try
            {
                var usercontext = await _context.Accounts.SingleAsync(a => a.Email == user);
                if (usercontext == null)
                {
                    throw new ArgumentNullException("user not found!");
                }

                await _context.SaveChangesAsync();
                logger.LogInformation("success!");
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                throw new Exception("failed to update account!");
            }
        }
    }
