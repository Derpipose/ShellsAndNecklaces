using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using ShellsAndNecklacesApp.Pages;

namespace ShellAndNecklaceIntegrations
{
    public class ShopIntegrationTest : BlazorIntegrationTestContext
    {
        [Fact]
        public async Task ItemPageCanAddToCart()
        {
            var cut = RenderComponent<ItemPage>();

            cut.Find("")
        }
    }
}
