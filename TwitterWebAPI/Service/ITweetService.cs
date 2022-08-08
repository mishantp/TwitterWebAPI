using TwitterWebAPI.Models;

namespace TwitterWebAPI.Service
{
    public interface ITweetService
    {
        Task<Response<Tweet>> AddTweet(Tweet TweetObject, string userName);
        Task<List<Tweet>> GetAllTweet();
        Task<bool> DeleteTweet(int TweetId, string userName);
        Task<List<Tweet>> GetAllTweetByUser(string userName);
        Task<Tweet> UpdateTweet(Tweet TweetObject, string userName);
        Task<Tweet> GetTweetById(int TweetId);
        Task<Response<TweetLike>> AddTweetLike(string UserName, int TweetId);
        Task<Response<TweetComment>> AddTweetReply(string UserName, int TweetId, bool IntitalComment, string Message);
    }
}
