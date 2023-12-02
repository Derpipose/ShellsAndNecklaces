using ShellAndNecklaceAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using ShellAndNecklaceAPI.Data;
using ShellAndNecklaceAPI.Data.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace ShellAndNecklaceAPI.Services
{
    public class ReviewService
    {
        private readonly ILogger<ReviewService> logger;
        private OneShotShopContext _Context;

        public async Task CreateItemReview(ItemReviewDTO itemrev)
        {
            logger.LogInformation("Item review creation started...");
            var itemid = _Context.Items.SingleOrDefaultAsync(i => i.Itemname == itemrev.ItemName).Id;
            var accid = _Context.Accounts.SingleOrDefaultAsync(a => a.Username == itemrev.Username).Id;

            if (itemid == null)
            {
                logger.LogError("Item review creation failed at" + DateTime.Now.ToString() + "! Item not found!");
                throw new KeyNotFoundException("Item not found!");
            }
            if(accid == null)
            {
                logger.LogError("Item review creation failed at" + DateTime.Now.ToString() + "! Account not found!");
                throw new KeyNotFoundException("Account not found!");
            }

            var newitemreview = new ItemReview()
            {
                Accountid = accid,
                Itemid = itemid,
                Rating = itemrev.Rating,
                Reviewdate = itemrev.ReviewDate,
                Reviewtext = itemrev.ItemReviewText
            };
            _Context.ItemReviews.Add(newitemreview);
            logger.LogInformation("Item review added to context.");
            await _Context.SaveChangesAsync();
            logger.LogInformation("Changes saved.");
            return;
        }

        public async Task<ItemReviewDTO> GetSingleItemReview()
        {
            logger.LogInformation("Item review retrieval initiated.");
            var latestItemReview = _Context.ItemReviews.MaxBy(ir => ir.Rating);
            if (latestItemReview == null)
            {
                logger.LogError("Item review retrieval failed at" + DateTime.Now.ToString() + "! No item reviews found!");
                throw new KeyNotFoundException("No item reviews found!");
            }
            var itemofinterest = await _Context.Items.SingleOrDefaultAsync(i => i.Id == latestItemReview.Itemid);
            if (itemofinterest == null)
            {
                logger.LogError("Item review retrieval failed at" + DateTime.Now.ToString() + "! Item not found!");
                throw new KeyNotFoundException("Item not found!");
            }
            var picstring = from p in _Context.Pictures
                            join ft in _Context.Filetypes on p.Filetypeid equals ft.Id
                            where itemofinterest.Pictureid == p.Id
                            select new {
                                pstr = p.Imagename + ft.Fileextension
                            };
            logger.LogInformation("Picture file retrieved.");
            if (picstring.First().ToString() == null)
            {
                logger.LogError("Item review creation failed at" + DateTime.Now.ToString() + "! Picture details were null!");
                throw new KeyNotFoundException("Picture file text could not be retrieved");
            }

            var accid = await _Context.Accounts.SingleOrDefaultAsync(a => a.Id == latestItemReview.Accountid);
            if (accid == null)
            {
                logger.LogError("Item review creation failed at" + DateTime.Now.ToString() + "! Account not found!");
                throw new KeyNotFoundException("Account not found!");
            }

            var itemreview = new ItemReviewDTO()
            {
                ItemName = itemofinterest.Itemname,
                ItemPicture = picstring.First().ToString(),
                ItemReviewText = latestItemReview.Reviewtext,
                Username = accid.Username,
                ReviewDate = latestItemReview.Reviewdate,
                Rating = latestItemReview.Rating
            };

            logger.LogInformation("Item review retrieval successful. Returning...");
            return itemreview;
        }

        public async Task CreateReview(ReviewDTO review)
        {
            logger.LogInformation("Attempting to add new review...");

            var Accid = await _Context.Accounts.SingleOrDefaultAsync(a => a.Username == review.Username);
            if (Accid == null)
            {
                logger.LogError("Review creation failed at" + DateTime.Now.ToString() + "! Account not found!");
                throw new KeyNotFoundException("Account not found!");
            }

            var newrev = new Review(){
                Accountid = Accid.Id,
                Rating = review.RatingScore,
                Reviewbody = review.ReviewText,
                Reviewdate = review.ReviewDate
            };
            _Context.Reviews.Add(newrev);
            await _Context.SaveChangesAsync();
            return;
        }

        public async Task<List<ReviewDTO>> GetSomeReviews(int count)
        {
            logger.LogInformation("Attempting retrieval of a few reviews...");
            List<ReviewDTO> reviewset = (List<ReviewDTO>)from r in _Context.Reviews
                                        join a in _Context.Accounts on r.Accountid equals a.Id
                                        orderby r.Rating descending
                                        select new ReviewDTO()
                                        {
                                            Username = a.Username,
                                            RatingScore = r.Rating,
                                            ReviewDate = (DateTime)r.Reviewdate,
                                            ReviewText = r.Reviewbody
                                        };

            if (reviewset.IsNullOrEmpty())
            {
                logger.LogError("Review bunch retrieval failed at" + DateTime.Now.ToString() + "! Empty list, no reviews found!");
                throw new KeyNotFoundException("No reviews found!");
            }

            return (List<ReviewDTO>)reviewset.Take(count);
        }
    }
}