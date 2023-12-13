using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using ShellAndNecklaceAPI.Data;
using ShellsAndNecklacesApp.Pages;
using ShellsAndNecklacesApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellAndNecklaceIntegrations
{
    public class AddReviewTest: BlazorIntegrationTestContext
    {
        [Fact]
        public async Task UserCanSubmitItemReview()
        {
            //navigate to the form
            var nav = Services.AddSingleton<NavigationManager>(new TestNav());
            var main = RenderComponent<MainLayout>();

            await main.Find("#reviews-link").ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

            //test first redirect
            //nav.Uri.Should().Be("http://localhost/reviews");

            //arrange
            using var dbContext = Services.GetRequiredService<OneShotShopContext>();
            var cut = RenderComponent<AddItemReview>();
            string userName = "testUser1";
            string itemName = "testItem1";
            int rating = 7;
            string text = "This is sample review text";

            cut.Find("#review-username").Change(userName);
            cut.Find("#shop-item").Change(itemName);
            cut.Find("#review-rating").Change(rating);
            cut.Find("#review-text").Change(text);
            await cut.Find("button.btn-primary").ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

            var newReviews = from review in dbContext.ItemReviews
                            where review.Reviewtext == text
                            where review.Rating == 7
                            select review;
            var newReview = newReviews.FirstOrDefault();

            newReview.Should().NotBeNull();
        }
            
    }
    internal class TestNav : NavigationManager
    {
        public TestNav()
        {
            Initialize("https://localhost/", "https://localhost/");
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            NotifyLocationChanged(false);
        }
    }
}
