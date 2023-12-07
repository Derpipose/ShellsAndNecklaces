using Blazorise;
using Microsoft.Extensions.Logging;
using ShellAndNecklaceAPI.Services;
using ShellAndNecklaceAPI.Data.DTOs;
using ShellAndNecklaceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellAndNecklaceUnitTests
{
	public class FakePurchaseLineController
	{
		/*private List<Purchaseorder> _purchases;
		public FakePurchaseLineController()
		{
			_purchases = new List<Purchaseorder>();
		}
		public void MakeItemList(List<Purchaseorder> newlist)
		{
			_purchases.AddRange(newlist);
		}

		public PurchasedItemDTO GetPL(string id)
		{
			int index = 0;
			if (!Int32.TryParse(id, out index))
			{
				throw new BadInputException();
			}

			foreach (var purchase in _purchases)
			{
				if (purchase.Id == index) return new PurchasedItemDTO()
				{ 
					
				};
			}

			throw new ElementNotFoundException("Purchase order not in list");
		}

		public List<PurchasedItemDTO> GetPLs()
		{
			List<PurchasedItemDTO> list = new List<PurchasedItemDTO>();
			foreach (var purchase in _purchases)
			{
				switch (purchase.Id % 4)
				{
					case 0:
						{
							list.Add(new PurchasedItemDTO()
							{
								//add in more upon completion of the controller
							});
						}
						break;
					case 1:
						{
							list.Add(new PurchasedItemDTO()
							{
								
							});
						}
						break;
					case 2:
						{
							list.Add(new PurchasedItemDTO()
							{
								
							});
						}
						break;
					default:
						{
							list.Add(new PurchasedItemDTO()
							{
								
							});
						}
						break;
				}
			}

			return list;
		}
	}
	public class FakePurchaseLineService
	{
		private readonly ILogger<FakePurchaseLineService> logger;
		private readonly FakePurchaseLineController PLService;
		public FakePurchaseLineService(ILogger<FakePurchaseLineService> logger, FakePurchaseLineController service)
		{
			this.logger = logger;
			PLService = service;
		}
		public List<PurchasedItemDTO> GetPLs()
		{
			return PLService.GetPLs();
		}
		public PurchasedItemDTO GetSinglePL(string id)
		{
			return PLService.GetPL(id);
		}


	}

	public class PLServiceTests : BlazorUnitTestContext
	{
		[Fact]
		public void PLServiceGetWorks()
		{
			//arrange
			var logmoq = Mock.Of<ILogger<PurchaseHistorySerice>>();
			var dbmoq = Mock.Of<FakePurchaseLineController>();
			//dbmoq.Setup(d => d.Get(It.IsAny<string>())).Returns(new PurchasedItemDTO);
			List<Purchaseorder> PLList = new List<Purchaseorder>();
			for (int i = 0; i < 10; i++)
			{
				
			}

			//act
			PurchasedItemDTO testItem = new PurchasedItemDTO();


			//assert
		}*/
	}
}
