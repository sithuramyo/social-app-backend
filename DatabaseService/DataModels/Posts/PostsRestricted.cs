namespace DatabaseService.DataModels.Posts;

[Table("Tbl_PostsRestricted")]
public class PostsRestricted
{
    [Key]
    public long Id { get; set; }
    public string PostsId { get; set; }
    public string PostsUserId { get; set; }
    public string PostsSharedUserId { get; set; }
}