using Microsoft.Extensions.Logging;
using ShellAndNecklaceAPI.Controllers;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellAndNecklaceUnitTests
{
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
			if (!Int32.TryParse(id, out index))
			{
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
				switch (item.Id % 4)
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

	public class ItemServiceTests : BlazorUnitTestContext
	{
		[Fact]
		public void ItemServiceGetWorks()
		{
			//arrange
			var logmoq = new Mock<ILogger<ItemService>>();
			var dbmoq = new Mock<FakeItemController>();
			dbmoq.Setup(d => d.GetItem(It.IsAny<string>())).Returns(new ItemDTO());
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
				var priceBase = (decimal)Math.Sqrt(i * 10);
				itemList.Add(new Item()
				{
					Id = i,
					Itemname = nameVal,
					Description = description,
					Pricebase = priceBase
					//as a list of items it has a few non-matching variables
					//likely needs definition for "statusId", "picId", etc.
				});
			}

			ItemDTO testItem = new ItemDTO()
			{
				Name = "A",
				Description = "AAAAAAAAAAA",
				Status = "AVAILABLE",
				PriceBase = (decimal)Math.Sqrt(10),
				PicString = "fireworks.jpeg"
			};
			//act
			//dbmoq.GetItem("id");

			//assert
			
		}
	}
}
