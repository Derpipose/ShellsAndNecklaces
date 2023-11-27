using ShellAndNecklaceAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Services
{
    public class ReviewController
    {
        private readonly ILogger<ReviewController> logger;
        private OneShotShopContext _Context;

        public async Task CreateReview(Review review)
        {
            _Context.Reviews.Add(review);
            await _Context.SaveChangesAsync();
            return;
        }

        public async Task<List<ReviewDTO>> GetSomeReviews()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteReview()
        {
            throw new NotImplementedException();
        }
    }
}