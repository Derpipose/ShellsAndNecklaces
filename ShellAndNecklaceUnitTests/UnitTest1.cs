using Castle.Core.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.Extensions.Logging;
using ShellAndNecklaceAPI.Controllers;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;
using ShellsAndNecklacesApp.Pages;
using ShellsAndNecklacesApp.Shared;
using System.Text;

namespace ShellAndNecklaceUnitTests
{
	public class BlazorUnitTestContext : TestContext
	{
		public BlazorUnitTestContext()
		{
			
		}
	}
	public class UITests : BlazorUnitTestContext
    {
        [Fact]
        public void AuthorizationCorrectlyValidatesUser()
        {
            var loginContext = new Login();

            //Code for example, not working yet
            //loginContext.username = "admin";
            //loginContext.password = "111111"
            //var cut = RenderComponent<Login>();
        }

        [Fact]
        public void ButtonsRedirectCorrectly() {
			//arrange
			using var ctx = new TestContext();
			var cut = RenderComponent<MainLayout>();
			//	var sfbutton = cut.FindComponent<MenuItem>;
			//act
			//assert
		}
    }

	public class FakeItemController
	{
		private List<Item> _items;
		public FakeItemController() 
		{ 
			_items = new List<Item>();
		}
		public void MakeItemList(List<Item> newlist)
		{
			_items.AddRange(newlist);
		}

		public ItemDTO GetItem(string id)
		{
			int index = 0;
			if(!Int32.TryParse(id, out index)){
				throw new BadInputException();
			}

			foreach (var item in _items)
			{
				if (item.Id == index) return new ItemDTO()
				{
					Description = item.Description,
					Name = item.Itemname,
					Status = "AVAILABLE",
					PicString = "fireworks.jpeg",
					PriceBase = (decimal)item.Pricebase
				};
			}

			throw new ElementNotFoundException("Item not in list.");
		}

		public List<ItemDTO> GetItems()
		{
			List<ItemDTO> list = new List<ItemDTO>();
			foreach (var item in _items)
			{
				switch (item.Id%4)
				{
					case 0:
						{
                            list.Add(new ItemDTO()
                            {
                                Name = item.Itemname,
                                Description = item.Description,
                                PicString = "fireworks.jpg",
                                PriceBase = (decimal)item.Pricebase,
								Status = "AVAILABLE"
                            });
                        }
						break;
					case 1:
						{
                            list.Add(new ItemDTO()
                            {
                                Name = item.Itemname,
                                Description = item.Description,
                                PicString = "fireworks.jpg",
                                PriceBase = (decimal)item.Pricebase,
								Status = "OUT_OF_STOCK"
                            });
                        }
						break;
					case 2:
						{
                            list.Add(new ItemDTO()
                            {
                                Name = item.Itemname,
                                Description = item.Description,
                                PicString = "fireworks.jpg",
                                PriceBase = (decimal)item.Pricebase,
								Status = "DISCONTINUED"
                            });
                        }
						break;
					default:
						{
                            list.Add(new ItemDTO()
                            {
                                Name = item.Itemname,
                                Description = item.Description,
                                PicString = "fireworks.jpg",
                                PriceBase = (decimal)item.Pricebase,
								Status = "PREVIEW"
                            });
                        }
						break;
				}
			}

			return list;
		}
	}
	public class FakeItemService
	{
		private readonly ILogger<FakeItemService> logger;
		private readonly FakeItemController itemService;
        public FakeItemService(ILogger<FakeItemService> logger, FakeItemController service)
        {
			this.logger = logger;
			itemService = service;
        }
		public List<ItemDTO> GetItems()
		{
			return itemService.GetItems();
		}
		public ItemDTO GetSingleItem(string id)
		{
			return itemService.GetItem(id);
		}
		
		
    }
	
	public class ServiceTests: BlazorUnitTestContext
	{
		[Fact]
		public void ItemServiceGetWorks()
		{
			//arrange
			var logmoq = Mock.Of<ILogger<ItemService>>();
			var dbmoq = Mock.Of<FakeItemController>();
			//dbmoq.Setup(d => d.Get(It.IsAny<string>())).Returns(new ItemDTO);
			List<Item> itemList = new List<Item>();
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
				var priceBase = (decimal)Math.Sqrt(i*10);
            }

			//act
			ItemDTO testItem = new ItemDTO();


			//assert
		}
	}
}