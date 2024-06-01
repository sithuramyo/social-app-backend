namespace DatabaseService.DataModels.Posts;

[Table("Tbl_Posts")]
public class Post : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string PostId { get; set; }
    public string UserId { get; set; }
    public string Caption { get; set; }
    public int PostAccessType { get; set; }
}