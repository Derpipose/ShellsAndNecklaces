using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using ShellsAndNecklacesApp.Pages;

namespace ShellAndNecklaceUnitTests
{
    public class ReviewComponentTest : TestContext
    {
        [Fact]
        public void ReviewComponentPopupIsNotOpenOnInitialized()

        {
            //arrage
            var cut = RenderComponent<ShellsAndNecklacesApp.Shared.ItemReview>();

            //assert
            cut.Find(".review-wrap .toast-container.d-none").Should().NotBeNull();
        }
        [Fact]
        public void ReviewComponentOpensPopUpOnClick()
            
        {
            //arrage
            var cut = RenderComponent<ShellsAndNecklacesApp.Shared.ItemReview>();

            //act
            cut.Find(".review").Click();

            //assert
            cut.Find(".review-wrap .toast-container.d-block").Should().NotBeNull();
        }
        [Fact]
        public void ReviewComponentShortensTextForPreview()
        {
            //arrange
            string fullText = "Text with more than 10 words that stops right here. This part of the comment should not be on the Short Version";
            var cut = RenderComponent<ShellsAndNecklacesApp.Shared.ItemReview>(parameters => parameters
            .Add(p => p.FullReviewText, fullText)
            );

            //assert
            cut.Find(".review-text").TextContent.Should().Be("Text with more than 10 words that stops right here....");
        }

        [Fact]
        public void ReviewComponentShowsStarRepresentationCorrectly()
        {
            //arrange
            //Rating of 9 is represented with 4 full-start and 1 half-star
            int rating = 9;
            var cut = RenderComponent<ShellsAndNecklacesApp.Shared.ItemReview>(parameters => parameters
            .Add(p => p.Rating, rating)
            );

            var fullStars = cut.FindAll(".bi-star-fill");
            var halfStars = cut.FindAll(".bi-star-half");

            //assert
            fullStars.Count.Should().Be(4);
            halfStars.Count.Should().Be(1);

        }

    }
}
