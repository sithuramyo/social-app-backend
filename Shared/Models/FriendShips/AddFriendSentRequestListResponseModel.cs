using Shared.Response;

namespace Shared.Models.FriendShips;

public class AddFriendSentRequestListResponseModel : BaseSubResponseModel
{
    public List<FriendShipsListModel> AddFriendRequestList { get; set; } = [];
}