namespace Shared.Models.Posts;

public class GetPostsModel
{
    public string PostId { get; set; }
    public string Captions { get; set; }
    public DateTime? PostedDate { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int ShareCount { get; set; }
    public List<PostMediaListModel> PostMediaList { get; set; } = [];
}