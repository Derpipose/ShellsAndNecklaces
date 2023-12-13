using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using ShellsAndNecklacesApp.Pages;

namespace ShellAndNecklaceIntegrations
{
    public class ShopIntegrationTest : BlazorIntegrationTestContext
    {
        [Fact]
        public async Task ItemPageCanAddToCart()
        {
            throw new NotImplementedException();

            var cut = RenderComponent<ItemPage>();

            cut.Find("form-select").Input(1);

            cut.Find("btn btn-primary").Click();

            cut.Find("cart-message").MarkupMatches("<p>Item added to cart!<p>");
        }
    }
}
