namespace DatabaseService.DataModels.Friends;

[Table("Tbl_FriendShips")]
public class Friendships
{
    [Key]
    public long Id { get; set; }
    public string SenderUserId { get; set; }
    public string ReceiverUserId { get; set; }
    public int FriendShipStatus { get; set; }
    public DateTime SendDate { get; set; }
    public DateTime ApprovedDate { get; set; }
}