using Shared.Models.FriendShips;

namespace SocialMediaService.Features.Friends;

public interface IFriendShipsService
{
    Task<GetFriendShipsListResponseModel> GetFriendList(string userId,CancellationToken ct);
    Task<SearchFriendListResponseModel> SearchFriendList(SearchFriendListRequestModel request,CancellationToken ct);
    Task<AddFriendSentRequestListResponseModel> GetFriendSentRequestList(string userId,CancellationToken ct);
    Task<AddFriendSentResponseModel> AddFriendSentRequest(string userId,AddFriendSentRequestModel request,CancellationToken ct);
    Task<ApproveFriendSentResponseModel> ApproveFriendSentRequest(string userId,ApproveFriendSentRequestModel request,CancellationToken ct);
    Task<UnFriendResponseModel> UnFriend(string userId,UnFriendRequestModel request,CancellationToken ct);
}