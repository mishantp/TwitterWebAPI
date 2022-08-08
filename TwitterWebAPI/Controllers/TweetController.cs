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

        [HttpPost("{userName}/like/{id}")]
        public async Task<IActionResult> AddLike(string userName, int id)
        {
            var response = await _TweetService.AddTweetLike(userName, id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("{userName}/reply/{id}")]
        public async Task<IActionResult> AddTweetReply(string userName, int id, bool intitalComment, string message)
        {
            var response = await _TweetService.AddTweetReply(userName, id, intitalComment, message);
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

        [HttpPost("{userName}/add")]
        public async Task<IActionResult> AddTweet(string userName, Tweet tweet)
        {
            var response = await _TweetService.AddTweet(tweet, userName);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{userName}/udpate/{id}")]
        public async Task<IActionResult> Update(string userName, int id, Tweet tweet)
        {
            var response = await _TweetService.UpdateTweet(tweet, userName);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{userName}/delete/{id}")]
        public async Task<IActionResult> Delete(string userName, int id)
        {
            var response = await _TweetService.DeleteTweet(id, userName);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("{userName}/all")]
        public async Task<IActionResult> Get(string userName)
        {

            var response = await _TweetService.GetAllTweetByUser(userName);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
