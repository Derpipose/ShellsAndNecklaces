using ShellsAndNecklacesApp.Pages;

namespace ShellAndNecklaceUnitTests
{
	public class BlazorUnitTestContext : TestContext
	{
		public BlazorUnitTestContext()
		{
			//register services here:

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
            var cut = RenderComponent<ShellsAndNecklacesApp.Pages.Index>();
        }
    }
}