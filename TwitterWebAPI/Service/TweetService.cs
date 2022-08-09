using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Data;
using TwitterWebAPI.Models;

namespace TwitterWebAPI.Service
{
    public class TweetService : ITweetService
    {
        public readonly AppDbContext _appDbContext;

        public TweetService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response<Tweet>> AddTweet(Tweet tweet, string userName)
        {
            var response = new Response<Tweet>();
            if (string.IsNullOrEmpty(userName))
            {
                response.Message = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == userName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Username doen't exist.";
                }
                else
                {
                    var createduser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == userName.ToLower());
                    tweet.CreatedDate = DateTime.Now;
                    tweet.UserId = createduser.Id;
                    _appDbContext.Tweets.Add(tweet);
                    _appDbContext.SaveChangesAsync();
                    response.Success = true;
                    response.Result = tweet;
                }
            }
            return response;
        }

        public async Task<Response<TweetLike>> AddTweetLike(string username, int tweetId)
        {
            var response = new Response<TweetLike>();
            if (string.IsNullOrEmpty(username))
            {
                response.Message = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == username.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Username not exist.";
                }
                else
                {
                    if (user != null)
                    {
                        TweetLike tweetLike = new TweetLike();
                        tweetLike.UserId = user.Id;
                        tweetLike.TweetId = tweetId;
                        tweetLike.LikeCount = 1;
                        _appDbContext.Add(tweetLike);
                        _appDbContext.SaveChangesAsync();
                        response.Success = true;
                        response.Result = tweetLike;
                    }
                }
            }
            return response;
        }

        public async Task<Response<TweetComment>> AddTweetReply(string username, int tweetId, bool intitalComment, string tweetMessage)
        {
            var response = new Response<TweetComment>();
            if (string.IsNullOrEmpty(username))
            {
                response.Message = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == username.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Username not exist.";
                }
                else
                {
                    var TweetCommentOject = _appDbContext.TweetComments.FirstOrDefault(t => t.TweetId == tweetId);
                    TweetComment TweetComment = new TweetComment();
                    if (!intitalComment && TweetCommentOject != null)
                    {
                        TweetComment.ParentCommentId = TweetCommentOject.Id;
                    }
                    if (user != null)
                    {
                        TweetComment.UserId = user.Id;
                        TweetComment.TweetId = tweetId;
                        TweetComment.Message = tweetMessage;
                        _appDbContext.Add(TweetComment);
                        _appDbContext.SaveChangesAsync();
                        response.Success = true;
                        response.Result = TweetComment;
                    }
                }
            }
            return response;           
        }

        public async Task<bool> DeleteTweet(int TweetId, string userName)
        {
            var tweet = await _appDbContext.Tweets.FirstOrDefaultAsync(t => t.Id == TweetId);
            _appDbContext.Tweets.Remove(tweet);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Tweet>> GetAllTweet()
        {
            try
            {
                return await _appDbContext.Tweets.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Tweet>> GetAllTweetByUser(string userName)
        {
            var user = _appDbContext.Users.FirstOrDefault(a => a.LoginId == userName);
            return await _appDbContext.Tweets.Where(a => a.UserId == user.Id).ToListAsync();
        }

        public async Task<Tweet> GetTweetById(int TweetId)
        {
            var tweetObj = _appDbContext.Tweets.FirstOrDefaultAsync(a => a.Id == TweetId);
            return await tweetObj;
        }

        public async Task<Tweet> UpdateTweet(Tweet tweet, string userName)
        {
            tweet.ModifiedDate = DateTime.Now;
            _appDbContext.Tweets.Update(tweet);
            _appDbContext.SaveChanges();
            return tweet;
        }
    }
}
