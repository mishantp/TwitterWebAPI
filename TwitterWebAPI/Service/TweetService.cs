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

        public async Task<Response<Tweet>> AddTweet(Tweet TweetObject, string userName)
        {
            var response = new Response<Tweet>();
            if (string.IsNullOrEmpty(userName))
            {
                response.ErrorMessage = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == userName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Username not exist.";
                }
                else
                {
                    var createduser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == userName.ToLower());
                    TweetObject.CreatedDate = DateTime.Now;
                    TweetObject.UserId = createduser.Id;
                    _appDbContext.Tweets.Add(TweetObject);
                    _appDbContext.SaveChangesAsync();
                    response.Success = true;
                    response.Result = TweetObject;
                }
            }
            return response;
        }

        public async Task<Response<TweetLike>> AddTweetLike(string UserName, int TweetId)
        {
            var response = new Response<TweetLike>();
            if (string.IsNullOrEmpty(UserName))
            {
                response.ErrorMessage = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == UserName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Username not exist.";
                }
                else
                {
                    if (user != null)
                    {
                        TweetLike TweetLike = new TweetLike();
                        TweetLike.UserId = user.Id;
                        TweetLike.TweetId = TweetId;
                        TweetLike.LikeCount = 1;
                        _appDbContext.Add(TweetLike);
                        _appDbContext.SaveChangesAsync();
                        response.Success = true;
                        response.Result = TweetLike;
                    }
                }
            }
            return response;
        }

        public async Task<Response<TweetComment>> AddTweetReply(string UserName, int TweetId, bool IntitalComment, string Message)
        {
            var response = new Response<TweetComment>();
            if (string.IsNullOrEmpty(UserName))
            {
                response.ErrorMessage = "Username must be required.";
                response.Success = false;
            }
            else
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.LoginId.ToLower() == UserName.ToLower());
                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Username not exist.";
                }
                else
                {
                    var TweetCommentOject = _appDbContext.TweetComments.FirstOrDefault(TweetComment => TweetComment.TweetId == TweetId);
                    TweetComment TweetComment = new TweetComment();
                    if (!IntitalComment && TweetCommentOject != null)
                    {
                        TweetComment.ParentCommentId = TweetCommentOject.Id;
                    }
                    if (user != null)
                    {
                        TweetComment.UserId = user.Id;
                        TweetComment.TweetId = TweetId;
                        TweetComment.Message = Message;
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

        public async Task<Tweet> UpdateTweet(Tweet TweetObject, string userName)
        {
            TweetObject.ModifiedDate = DateTime.Now;
            _appDbContext.Tweets.Update(TweetObject);
            _appDbContext.SaveChanges();
            return TweetObject;
        }
    }
}
