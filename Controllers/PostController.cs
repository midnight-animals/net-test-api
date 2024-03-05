using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using net_test_api.Models;
using net_test_api.Services;

namespace net_test_api.Controllers;

[Controller]
[Route("api/[controller]")]
public class PostController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public PostController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Post>> GetPosts()
    {
        return await _mongoDBService.GetPostsAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        await _mongoDBService.CreateAsync(post);
        return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, post);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditGivePost(string id, [FromBody] Post post)
    {
        await _mongoDBService.UpdatePostAsync(id, post);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGivenPost(string id)
    {
        await _mongoDBService.DeletePostAsync(id);
        return NoContent();
    }
}
