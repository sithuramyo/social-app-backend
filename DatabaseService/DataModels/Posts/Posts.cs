namespace DatabaseService.DataModels.Posts;

[Table("Tbl_Posts")]
public class Posts : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string PostId { get; set; }
    public string UserId { get; set; }
    public string Context { get; set; }
    public string? PostImagePath { get; set; }
}