using ShellAndNecklaceAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;

namespace ShellAndNecklaceAPI.Services
{
    public class ReviewService
    {
        private readonly ILogger<ReviewService> logger;
        private OneShotShopContext _Context;

        public async Task CreateReview(Review review)
        {
            _Context.Reviews.Add(review);
            await _Context.SaveChangesAsync();
            return;
        }

        public async Task<List<ReviewDTO>> GetSomeReviews()
        {
            List<ReviewDTO> reviewset = (List<ReviewDTO>)from r in _Context.Reviews
                                        join a in _Context.Accounts on r.Accountid equals a.Id
                                        orderby r.Rating
                                        select new ReviewDTO()
                                        {
                                            Username = a.Username,
                                            RatingScore = r.Rating,
                                            ReviewDate = (DateTime)r.Reviewdate,
                                            ReviewText = r.Reviewbody
                                        };

            return (List<ReviewDTO>)reviewset.Take(5);
        }
    }
}