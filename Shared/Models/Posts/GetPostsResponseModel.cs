using Shared.Response;

namespace Shared.Models.Posts;

public class GetPostsResponseModel : BaseSubResponseModel
{
    public List<GetPostsModel> PostList { get; set; } = [];
}