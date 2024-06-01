namespace DatabaseService.DataModels.Friends;

[Table("Tbl_FriendsBlocked")]
public class FriendBlocked : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string UserId { get; set; }
    public string BlockedUserId { get; set; }
}