namespace DatabaseService.DataModels.Posts;

[Table("Tbl_PostsLiked")]
public class PostsLiked : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string LikedId { get; set; }
    public string PostId { get; set; }
    public string UserId { get; set; }
}