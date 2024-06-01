using Shared.Response;

namespace Shared.Models.Posts;

public class CreatePostResponseModel : BaseSubResponseModel
{
    public string PostId { get; set; }
    public string Caption { get; set; }
    public List<PostMediaResponseModel> PostMediaPaths { get; set; } = [];
}