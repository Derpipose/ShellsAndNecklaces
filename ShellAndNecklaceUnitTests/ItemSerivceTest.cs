using Microsoft.Extensions.Logging;
using ShellAndNecklaceAPI.Services;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;

namespace ShellAndNecklaceUnitTests
{
	public static class TestHelper
	{
		public static List<Item> GetFakeItemList()
		{
			return new List<Item>()
			{
				new Item
				{
					Id = 1,
					Description = null,
					Itemname = "Wood Cutout Large",
					Pictureid = 1,
					Statusid = 1
				},
				new Item{
                    Id = 2,
					Description = null,
                    Itemname = "12 gauge",
                    Pictureid = 2,
                    Statusid = 1
                },
				new Item
				{
                    Id = 3,
					Description = null,
                    Itemname = "Wood Cutout Small",
                    Pictureid = 1,
                    Statusid = 2
                }
			};
		}

		public static List<Picture> GetFakePictures()
		{
			return new List<Picture>()
			{
				new Picture
				{
					Id=1,
					Filetypeid=1,
					Imagename="wood"
				},
				new Picture
				{
                    Id=2,
                    Filetypeid=1,
                    Imagename="shell"
                }
			};
		}

		public static List<Filetype> GetFakeFileTypes()
		{
			return new List<Filetype>()
			{
				new Filetype
				{
					Id=1,
					Fileextension=".jpg"
				}
			};
		}

		public static List<Status> GetFakeStatuses()
		{
			return new List<Status>()
			{
				new Status
				{
					Id=1,
					Status1="AVAILABLE"
				},
				new Status
				{
					Id=2,
					Status1="OUT_OF_STOCK"
				}
			};
		}
    }


	public class TestingItemController {

		[Fact]
		public async void GetReturnsASingleItem()
		{
			//Arrange
			var itemmock = TestHelper.GetFakeItemList().BuildMock().BuildMockDbSet();
			var picturemock = TestHelper.GetFakePictures().BuildMock().BuildMockDbSet();
			var filemock = TestHelper.GetFakeFileTypes().BuildMock().BuildMockDbSet();
			var statusmock = TestHelper.GetFakeStatuses().BuildMock().BuildMockDbSet();
			var loggerMoq = new Mock<ILogger<ItemService>>();

			var databaseMoq = new Mock<OneShotShopContext>();

			databaseMoq.Setup(x => x.Items).Returns(itemmock.Object);
			databaseMoq.Setup(x => x.Pictures).Returns(picturemock.Object);
			databaseMoq.Setup(x => x.Filetypes).Returns(filemock.Object);
			databaseMoq.Setup(x => x.Statuses).Returns(statusmock.Object);

			//Act
			var testservice = new ItemService(loggerMoq.Object, databaseMoq.Object);
			var newItem = await testservice.Get("Wood Cutout Large");

			//Assert
			Assert.NotNull(newItem);
			Assert.Equal(newItem.Name, "Wood Cutout Large");
			Assert.Equal(newItem.PicString, "wood.jpg");
			Assert.Equal(newItem.StatusType, "AVAILABLE");
        }

		[Fact]
		public async void GetAllReturnsAFullList()
		{
			//Arrange
			List<ItemDTO> items = new List<ItemDTO>()
			{
				new ItemDTO
				{
					Name="Wood Cutout Large",
					StatusType="AVAILABLE",
					PicString="wood.jpg"
				},
				new ItemDTO
				{
					Name="12 gauge",
					StatusType="AVAILABLE",
					PicString="shell.jpg"
				},
				new ItemDTO
				{
					Name="Wood Cutout Small",
					StatusType="OUT_OF_STOCK",
					PicString="wood.jpg"
				}
			};


            //Arrange
            var itemmock = TestHelper.GetFakeItemList().BuildMock().BuildMockDbSet();
            var picturemock = TestHelper.GetFakePictures().BuildMock().BuildMockDbSet();
            var filemock = TestHelper.GetFakeFileTypes().BuildMock().BuildMockDbSet();
            var statusmock = TestHelper.GetFakeStatuses().BuildMock().BuildMockDbSet();
            var loggerMoq = new Mock<ILogger<ItemService>>();

            var databaseMoq = new Mock<OneShotShopContext>();

            databaseMoq.Setup(x => x.Items).Returns(itemmock.Object);
            databaseMoq.Setup(x => x.Pictures).Returns(picturemock.Object);
            databaseMoq.Setup(x => x.Filetypes).Returns(filemock.Object);
            databaseMoq.Setup(x => x.Statuses).Returns(statusmock.Object);

            var service = new ItemService(loggerMoq.Object, databaseMoq.Object);

			//act
			var newListItems = await service.GetAll();

			//assert
			newListItems.Should().NotBeNull();
			for(int i = 0; i < newListItems.Count() && i < items.Count(); i++)
			{
				if (newListItems.Count() != items.Count())
					throw new IndexOutOfRangeException();
				
				Assert.Equivalent(newListItems[i], items[i]);
			}
        }
	}

}
