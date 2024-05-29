using Shared.Response;

namespace Shared.Models.FriendShips;

public class SearchFriendListResponseModel : BaseSubResponseModel
{
    public List<FriendShipsListModel> FriendShipsList { get; set; } = [];
}