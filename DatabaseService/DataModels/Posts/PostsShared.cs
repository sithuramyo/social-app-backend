namespace DatabaseService.DataModels.Posts;

[Table("Tbl_PostsShared")]
public class PostsShared : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string PostSharedId { get; set; }
    public string PostId { get; set; }
    public string OriginalPostId { get; set; }
}