using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterWebAPI.Models
{
    public class TweetLike
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Tweet")]
        public int TweetId { get; set; }
        public int LikeCount { get; set; }
    }
}
