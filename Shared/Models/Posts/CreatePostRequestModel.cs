namespace Shared.Models.Posts;

public class CreatePostRequestModel
{
    public string Caption { get; set; }
    public int PostAccessType { get; set; }
    public List<PostMediaRequestModel> PostMedia { get; set; } = [];
}