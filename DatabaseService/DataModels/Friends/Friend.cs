namespace DatabaseService.DataModels.Friends;

[Table("Tbl_Friends")]
public class Friend
{
    [Key]
    public long Id { get; set; }
    public string UserIdOne { get; set; }
    public string UserIdTwo { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsUnfriend { get; set; }
}