using Blazorise;
using Microsoft.Extensions.Logging;
using ShellAndNecklaceAPI.Controllers;
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
		private List<Purchaseorder> _purchases;
		public FakePurchaseLineController()
		{
			_purchases = new List<Purchaseorder>();
		}
		public void MakeItemList(List<Purchaseorder> newlist)
		{
			_purchases.AddRange(newlist);
		}

		public PurchaseLineDTO GetPL(string id)
		{
			int index = 0;
			if (!Int32.TryParse(id, out index))
			{
				throw new BadInputException();
			}

			foreach (var purchase in _purchases)
			{
				if (purchase.Id == index) return new PurchaseLineDTO()
				{ 
					
				};
			}

			throw new ElementNotFoundException("Purchase order not in list");
		}

		public List<PurchaseLineDTO> GetPLs()
		{
			List<PurchaseLineDTO> list = new List<PurchaseLineDTO>();
			foreach (var purchase in _purchases)
			{
				switch (purchase.Id % 4)
				{
					case 0:
						{
							list.Add(new PurchaseLineDTO()
							{
								//add in more upon completion of the controller
							});
						}
						break;
					case 1:
						{
							list.Add(new PurchaseLineDTO()
							{
								
							});
						}
						break;
					case 2:
						{
							list.Add(new PurchaseLineDTO()
							{
								
							});
						}
						break;
					default:
						{
							list.Add(new PurchaseLineDTO()
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
		public List<PurchaseLineDTO> GetPLs()
		{
			return PLService.GetPLs();
		}
		public PurchaseLineDTO GetSinglePL(string id)
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
			var logmoq = Mock.Of<ILogger<PurchaseLineService>>();
			var dbmoq = Mock.Of<FakePurchaseLineController>();
			//dbmoq.Setup(d => d.Get(It.IsAny<string>())).Returns(new PurchaseLineDTO);
			List<Purchaseorder> PLList = new List<Purchaseorder>();
			for (int i = 0; i < 10; i++)
			{
				
			}

			//act
			PurchaseLineDTO testItem = new PurchaseLineDTO();


			//assert
		}
	}
}
