using Shared.Models.Posts;

namespace SocialMediaService.Features.Posts;

[Route("socialmediaapi/[controller]")]
[ApiController]
public class PostController : BaseController
{
    private readonly IPostService _service;

    public PostController(IPostService service)
    {
        _service = service;
    }

    [HttpGet("get-friend-posts")]
    public async Task<IActionResult> GetFriendPosts(CancellationToken ct)
    {
        GetPostsResponseModel model = new();
        try
        {
            model = await _service.GetFriendPosts(GetUserId(), ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost(CreatePostRequestModel request, CancellationToken ct)
    {
        CreatePostResponseModel model = new();
        try
        {
            model = await _service.CreatePost(GetUserId(), request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("manage-post")]
    public async Task<IActionResult> ManagePost(ManagePostRequestModel request, CancellationToken ct)
    {
        ManagePostResponseModel model = new();
        try
        {
            model = await _service.ManagePost(GetUserId(), request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }
}