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
}