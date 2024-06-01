namespace DatabaseService.DataModels.Posts;

[Table("Tbl_PostsMedia")]
public class PostsMedia : BaseDataModel
{
    [Key] 
    public long Id { get; set; }
    public string PostsMediaId { get; set; }
    public string PostsId { get; set; }
    public string PostsMediaPath { get; set; }
}