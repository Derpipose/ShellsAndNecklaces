using Microsoft.Extensions.Logging;
using ShellAndNecklaceAPI.Services;
using ShellAndNecklaceAPI.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellAndNecklaceUnitTests
{
	public class FakeAccountController
	{
		private List<Account> _accounts;
		public FakeAccountController()
		{
			_accounts = new List<Account>();
		}
		public void MakeAccountList(List<Account> newlist)
		{
			_accounts.AddRange(newlist);
		}

		public AccountDTO GetAccount(string id)
		{
			int index = 0;
			if (!Int32.TryParse(id, out index))
			{
				throw new BadInputException();
			}

			foreach (var account in _accounts)
			{
				if (account.Id == index) return new AccountDTO()
				{
					Username = account.Username,
					Email = account.Email,
					Phone = account.Phone,
					Address = account.Address,
					Verified = account.Verified,
					Closed = account.Closed
				};
			}

			throw new ElementNotFoundException("Account not in list.");
		}

		public List<AccountDTO> GetAccounts()
		{
			List<AccountDTO> list = new List<AccountDTO>();
			foreach (var account in _accounts)
			{
				switch (account.Id % 4)
				{
					case 0:
						{
							list.Add(new AccountDTO()
							{
								Username = account.Username,
								Email = account.Email,
								Phone = account.Phone,
								Address = account.Address,
								Verified = true,
								Closed = false
							});
						}
						break;
					case 1:
						{
							list.Add(new AccountDTO()
							{
								Username = account.Username,
								Email = account.Email,
								Phone = account.Phone,
								Address = account.Address,
								Verified = false,
								Closed = false
							});
						}
						break;
					case 2:
						{
							list.Add(new AccountDTO()
							{
								Username = account.Username,
								Email = account.Email,
								Phone = account.Phone,
								Address = account.Address,
								Verified = true,
								Closed = true
							});
						}
						break;
					default:
						{
							list.Add(new AccountDTO()
							{
								Username = account.Username,
								Email = account.Email,
								Phone = account.Phone,
								Address = account.Address,
								Verified = false,
								Closed = true
							});
						}
						break;
				}
			}

			return list;
		}
	}
	public class FakeAccountService
	{
		private readonly ILogger<FakeAccountService> logger;
		private readonly FakeAccountController accountService;
		public FakeAccountService(ILogger<FakeAccountService> logger, FakeAccountController service)
		{
			this.logger = logger;
			accountService = service;
		}
		public List<AccountDTO> GetAccounts()
		{
			return accountService.GetAccounts();
		}
		public AccountDTO GetSingleAccount(string id)
		{
			return accountService.GetAccount(id);
		}


	}

	public class AccountServiceTests : BlazorUnitTestContext
	{
		[Fact]
		public void AccountServiceGetWorks()
		{
			//arrange
			var logmoq = Mock.Of<ILogger<AccountService>>();
			var dbmoq = Mock.Of<FakeAccountController>();
			//dbmoq.Setup(d => d.Get(It.IsAny<string>())).Returns(new AccountDTO);
			List<Account> accountList = new List<Account>();
			for (int i = 0; i < 10; i++)
			{
				var nameVal = (i + 96).ToString();
				var description = "";
				for (int j = 0; j < 10; j++)
				{
					description += nameVal;
				}
				var status = "AVAILABLE";
				var picString = "fireworks.jpeg";
				var priceBase = (decimal)Math.Sqrt(i * 10);
			}

			//act
			AccountDTO testAccount = new AccountDTO();


			//assert
		}
	}
}
