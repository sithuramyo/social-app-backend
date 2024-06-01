using Shared.Models.Posts;

namespace SocialMediaService.Features.Posts;

public interface IPostService
{
    Task<GetPostsResponseModel> GetFriendPosts(string userId, CancellationToken ct);
    Task<CreatePostResponseModel> CreatePost(string userId, CreatePostRequestModel request ,CancellationToken ct);
    Task<ManagePostResponseModel> ManagePost(string userId, ManagePostRequestModel request ,CancellationToken ct);
}