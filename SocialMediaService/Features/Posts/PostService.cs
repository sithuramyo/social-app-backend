using System.Linq.Dynamic.Core;
using AuthenticationService.Features.CommonServices;
using DatabaseService.AppContextModels;
using DatabaseService.DataModels.Posts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Shared.Constants;
using Shared.Extensions;
using Shared.Models.Posts;

namespace SocialMediaService.Features.Posts;

public class PostService : IPostService
{
    private readonly AppDbContext _context;
    private readonly UploadService _uploadService;

    public PostService(AppDbContext context, UploadService uploadService)
    {
        _context = context;
        _uploadService = uploadService;
    }

    public async Task<GetPostsResponseModel> GetFriendPosts(string userId, CancellationToken ct)
    {
        GetPostsResponseModel model = new();
        List<GetPostsModel> postList = [];
        var userIds = await (from user in _context.Users
            join friendBlocked in _context.FriendsBlocked on user.UserId equals friendBlocked.BlockedUserId into gj
            from subFriendBlocked in gj.DefaultIfEmpty()
            where subFriendBlocked == null && !subFriendBlocked.IsDeleted
            select new
            {
                user.UserId
            }).ToListAsync(ct);

        if (userIds.Count <= 0)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        foreach (var id in userIds)
        {
            GetPostsModel postModel = new();
            var post = await _context.Posts
                .FirstOrDefaultAsync(x =>
                    x.UserId == id.UserId &&
                    x.IsDeleted &&
                    x.PostAccessType != 4, ct);
            if (post is null) continue;
            var isContain = await _context.PostsRestricted
                .AnyAsync(x =>
                    x.PostsId == post.PostId &&
                    x.PostsUserId == id.UserId &&
                    x.PostsSharedUserId == userId, ct);
            if (!isContain) continue;
            var postLikeCount = await _context.PostsLiked
                .CountAsync(x =>
                    x.PostId == post.PostId &&
                    !x.IsDeleted, ct);
            var postCommentCount = await _context.PostsComments
                .CountAsync(x =>
                    x.PostId == post.PostId &&
                    !x.IsDeleted, ct);
            var postShareCount = await _context.PostsShared
                .CountAsync(x =>
                    x.PostId == post.PostId &&
                    !x.IsDeleted, ct);
            var postMedia = await _context.PostsMedia
                .Where(x => x.PostsId == post.PostId)
                .Select(x => new PostMediaListModel
                {
                    PostMediaId = x.PostsMediaId,
                    PostMediaPath = x.PostsMediaPath
                }).ToListAsync(ct);
            postModel.PostId = post.PostId;
            postModel.Captions = post.Caption;
            postModel.PostedDate = post.CreatedDate;
            postModel.LikesCount = postLikeCount;
            postModel.CommentsCount = postCommentCount;
            postModel.ShareCount = postShareCount;
            postModel.PostMediaList = postMedia;
            postList.Add(postModel);
        }

        model.PostList = postList;
        model.Response.Set(ResponseConstants.S0000);
        return model;
    }

    public async Task<CreatePostResponseModel> CreatePost(string userId, CreatePostRequestModel request,
        CancellationToken ct)
    {
        CreatePostResponseModel model = new();
        List<PostMediaResponseModel> postMediaPaths = [];
        if (request.Caption.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }

        var typeValue = request.PostAccessType.GetPostAccessType();

        if (typeValue == 0)
        {
            model.Response.Set(ResponseConstants.W0012);
            return model;
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            Post post = new()
            {
                PostId = Guid.NewGuid().ToString(),
                UserId = userId,
                Caption = request.Caption,
                PostAccessType = typeValue,
                CreatedDate = DateTime.Now
            };
            await _context.Posts.AddAsync(post, ct);
            var result = await _context.SaveChangesAsync(ct);
            if (result <= 0)
            {
                model.Response.Set(ResponseConstants.E0000);
                return model;
            }

            if (request.PostMedia.Count > 0)
            {
                foreach (var item in request.PostMedia)
                {
                    var filePath = await _uploadService.SavePostsMedia(item.PostMedia, Guid.NewGuid().ToString());
                    PostsMedia media = new()
                    {
                        PostsMediaId = Guid.NewGuid().ToString(),
                        PostsId = post.PostId,
                        PostsMediaPath = filePath,
                        CreatedDate = DateTime.Now
                    };

                    await _context.PostsMedia.AddAsync(media, ct);
                    var postMediaResult = await _context.SaveChangesAsync(ct);
                    if (postMediaResult <= 0) continue;
                    PostMediaResponseModel postMediaResponseModel = new()
                    {
                        PostMediaPath = media.PostsMediaPath
                    };
                    postMediaPaths.Add(postMediaResponseModel);
                }
            }

            model.PostId = post.PostId;
            model.Caption = post.Caption;
            model.PostMediaPaths = postMediaPaths;
            model.Response.Set(ResponseConstants.S0000);
            return model;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(ct);
            model.Response.Set(ResponseConstants.E0000);
            return model;
        }
    }

    public async Task<ManagePostResponseModel> ManagePost(string userId, ManagePostRequestModel request,
        CancellationToken ct)
    {
        ManagePostResponseModel model = new();

        var typeValue = request.PostAccessType.GetPostAccessType();

        if (typeValue == 0)
        {
            model.Response.Set(ResponseConstants.W0012);
            return model;
        }

        var posts = await _context.Posts
            .FirstOrDefaultAsync(x => x.UserId == userId && x.PostId == request.PostsId && !x.IsDeleted, ct);

        if (posts is null)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        posts.PostAccessType = typeValue;
        _context.Posts.Update(posts);
        var result = await _context.SaveChangesAsync(ct);

        if (result <= 0)
        {
            model.Response.Set(ResponseConstants.E0000);
            return model;
        }

        model.Response.Set(ResponseConstants.S0000);
        return model;
    }
}