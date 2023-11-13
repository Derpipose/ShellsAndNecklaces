

using ShellsAndNecklacesApp.Pages;

namespace ShellAndNecklaceUnitTests
{
    public class UITests
    {
        [Fact]
        public void AuthorizationCorrectlyValidatesUser()
        {
            var loginContext = new Login();
            var cut = RenderComponent<Login>();

        }
    }
}