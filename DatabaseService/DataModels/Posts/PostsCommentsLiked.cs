namespace DatabaseService.DataModels.Posts;

[Table("Tbl_PostsCommentsLiked")]
public class PostsCommentsLiked : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string PostsCommentsLikedId { get; set; }
    public string PostId { get; set; }
    public string PostCommentsId { get; set; }
    public string UserId { get; set; }
}