using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterWebAPI.Models
{
    public class TweetComment
    {
        public int Id { get; set; }
        public int ParentCommentId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Tweet")]
        public int TweetId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
