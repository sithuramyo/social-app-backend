namespace DatabaseService.DataModels.Posts;

[Table("Tbl_PostsComments")]
public class PostsComments : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string CommentId { get; set; }
    public string PostId { get; set; }
    public string UserId { get; set; }
    public string CommentText { get; set; }
    public string? CommentImagePath { get; set; }
}