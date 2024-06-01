using Shared.Models.FriendShips;

namespace SocialMediaService.Features.Friends;

[Route("socialmediaapi/[controller]")]
[ApiController]
public class FriendShipsController : BaseController
{
    private readonly IFriendShipsService _service;

    public FriendShipsController(IFriendShipsService service)
    {
        _service = service;
    }

    [HttpGet("get-friend-list")]
    public async Task<IActionResult> GetFriendList(CancellationToken ct)
    {
        GetFriendShipsListResponseModel model = new();
        try
        {
            model = await _service.GetFriendList(GetUserId(), ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("search-friend-list")]
    public async Task<IActionResult> SearchFriendList(SearchFriendListRequestModel request,
        CancellationToken ct)
    {
        SearchFriendListResponseModel model = new();
        try
        {
            model = await _service.SearchFriendList(request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpGet("get-friend-sent-request-list")]
    public async Task<IActionResult> GetFriendSentRequestList(CancellationToken ct)
    {
        AddFriendSentRequestListResponseModel model = new();
        try
        {
            model = await _service.GetFriendSentRequestList(GetUserId(), ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }


    [HttpPost("add-friend-request-sent")]
    public async Task<IActionResult> AddFriendRequestSent(AddFriendSentRequestModel request,
        CancellationToken ct)
    {
        AddFriendSentResponseModel model = new();
        try
        {
            model = await _service.AddFriendSentRequest(GetUserId(), request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("approve-friend-sent-request")]
    public async Task<IActionResult> ApproveFriendSentRequest(ApproveFriendSentRequestModel request,
        CancellationToken ct)
    {
        ApproveFriendSentResponseModel model = new();
        try
        {
            model = await _service.ApproveFriendSentRequest(GetUserId(), request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("un-friend")]
    public async Task<IActionResult> UnFriend(UnFriendRequestModel request,
        CancellationToken ct)
    {
        UnFriendResponseModel model = new();
        try
        {
            model = await _service.UnFriend(GetUserId(), request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("blocked-friend")]
    public async Task<IActionResult> BlockedFriend(BlockedFriendRequestModel request,
        CancellationToken ct)
    {
        BlockedFriendResponseModel model = new();
        try
        {
            model = await _service.BlockedFriend(GetUserId(), request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpPost("unblocked-friend")]
    public async Task<IActionResult> UnBlockedFriend(UnBlockedFriendRequestModel request,
        CancellationToken ct)
    {
        UnBlockedFriendResponseModel model = new();
        try
        {
            model = await _service.UnBlockedFriend(GetUserId(), request, ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }

    [HttpGet("get-blocked-friend-list")]
    public async Task<IActionResult> GetBlockedFriendList(CancellationToken ct)
    {
        BlockedFriendListResponseModel model = new();
        try
        {
            model = await _service.GetBlockedFriendList(GetUserId(), ct);
        }
        catch (Exception ex)
        {
            return SystemError(model, ex);
        }

        return OkWithLocalize(model);
    }
}