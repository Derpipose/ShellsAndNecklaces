using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using ShellsAndNecklacesApp.Pages;
using ShellsAndNecklacesApp.Shared;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Navigations;

namespace ShellAndNecklaceUnitTests
{
	public class BlazorUnitTestContext : TestContext
	{
		public BlazorUnitTestContext()
		{
			var builder = WebApplication.CreateBuilder();
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgxMzAwMUAzMjMzMmUzMDJlMzBVOHZLME1sZWpxVDRheHphTmozL2J4dm1seEpxVXc3cXh3dmhBRkVpSEFBPQ ==");
			builder.Services.AddServerSideBlazor();
			builder.Services.AddSyncfusionBlazor();
			//var mockService = new Mock<IService>();
			//mockService.Setup(m=>m.DoSomething()).Returns(5);
			//Services.AddScoped<IService>(_ => mockService.Object);
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