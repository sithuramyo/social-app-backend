using Shared.Response;

namespace Shared.Models.FriendShips;

public class GetFriendShipsListResponseModel : BaseSubResponseModel
{
    public List<FriendShipsListModel> FriendShipsList { get; set; } = [];
    public int FriendCount { get; set; }
}