using TwitterWebAPI.Models;

namespace TwitterWebAPI.Service
{
    public interface ITweetService
    {
        Task<Response<Tweet>> AddTweet(Tweet tweet, string username);
        Task<List<Tweet>> GetAllTweet();
        Task<bool> DeleteTweet(int TweetId, string username);
        Task<List<Tweet>> GetAllTweetByUser(string username);
        Task<Tweet> UpdateTweet(Tweet tweet, string username);
        Task<Tweet> GetTweetById(int TweetId);
        Task<Response<TweetLike>> AddTweetLike(string username, int TweetId);
        Task<Response<TweetComment>> AddTweetReply(string username, int tweetId, bool intitalComment, string tweetMessage);
    }
}
