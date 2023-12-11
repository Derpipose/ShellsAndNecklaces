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

namespace ShellAndNecklaceUnitTests
{

	public class TestingItemController {
		private Mock<Logger<ItemService>> MockLogger;
		private Mock<OneShotShopContext> MockContext;
		private ItemService _itemService;

		public TestingItemController() { 
			MockLogger = new Mock<Logger<ItemService>>();
			MockContext = new Mock<OneShotShopContext>();
			_itemService = new ItemService(MockLogger.Object, MockContext.Object);
		}
		
	}

}
