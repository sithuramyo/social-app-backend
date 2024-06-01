using Shared.Response;

namespace Shared.Models.FriendShips;

public class BlockedFriendListResponseModel : BaseSubResponseModel
{
    public List<BlockedFriendListModel> BlockedFriendList { get; set; } = [];
}