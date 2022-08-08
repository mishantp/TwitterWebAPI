using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterWebAPI.Models;
using TwitterWebAPI.Service;

namespace TwitterWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : Controller
    {
        private readonly ITweetService _TweetService;

        public TweetController(ITweetService tweetService)
        {
            _TweetService = tweetService;
        }

        [HttpPost("{username}/like/{id}")]
        public async Task<IActionResult> AddLike(string username, int id)
        {
            var response = await _TweetService.AddTweetLike(username, id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("{username}/reply/{id}")]
        public async Task<IActionResult> AddTweetReply(string username, int id, bool intitalComment, string message)
        {
            var response = await _TweetService.AddTweetReply(username, id, intitalComment, message);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var response = await _TweetService.GetAllTweet();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("{username}/add")]
        public async Task<IActionResult> AddTweet(string username, Tweet tweet)
        {
            var response = await _TweetService.AddTweet(tweet, username);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{username}/udpate/{id}")]
        public async Task<IActionResult> Update(string username, int id, Tweet tweet)
        {
            var response = await _TweetService.UpdateTweet(tweet, username);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{username}/delete/{id}")]
        public async Task<IActionResult> Delete(string username, int id)
        {
            var response = await _TweetService.DeleteTweet(id, username);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{username}/all")]
        public async Task<IActionResult> Get(string username)
        {

            var response = await _TweetService.GetAllTweetByUser(username);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
