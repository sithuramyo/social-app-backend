using System.Runtime.CompilerServices;
using DatabaseService.AppContextModels;
using DatabaseService.DataModels.Friends;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models.FriendShips;

namespace SocialMediaService.Features.Friends;

public class FriendShipsService : IFriendShipsService
{
    private readonly AppDbContext _context;

    public FriendShipsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetFriendShipsListResponseModel> GetFriendList(string userId, CancellationToken ct)
    {
        GetFriendShipsListResponseModel model = new();

        var friendList = await (from users in _context.Users
            join friend in _context.Friends on users.UserId equals friend.UserIdTwo
            where friend.UserIdOne == userId && !friend.IsUnfriend
            select new FriendShipsListModel
            {
                FriendId = users.UserId,
                Name = users.Name,
                FriendPhotoPath = users.ProfileImagePath ?? string.Empty
            }).ToListAsync(ct);

        if (friendList.Count <= 0)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        model.FriendShipsList = friendList;
        model.FriendCount = model.FriendShipsList.Count;
        model.Response.Set(ResponseConstants.S0000);
        return model;
    }

    public async Task<SearchFriendListResponseModel> SearchFriendList(SearchFriendListRequestModel request,
        CancellationToken ct)
    {
        SearchFriendListResponseModel model = new();

        if (request.Name.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }

        var friendList = await _context.Users
            .Where(x => x.Name.StartsWith(request.Name) && !x.IsDeleted)
            .Select(x => new FriendShipsListModel
            {
                FriendId = x.UserId,
                Name = x.Name,
                FriendPhotoPath = x.ProfileImagePath ?? string.Empty
            }).ToListAsync(ct);

        if (friendList.Count <= 0)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        model.FriendShipsList = friendList;
        model.Response.Set(ResponseConstants.S0000);
        return model;
    }

    public async Task<AddFriendSentRequestListResponseModel> GetFriendSentRequestList(string userId,
        CancellationToken ct)
    {
        AddFriendSentRequestListResponseModel model = new();

        var sentRequestFriends = await (from users in _context.Users
            join friendShip in _context.FriendShips on users.UserId equals friendShip.SenderUserId
            where friendShip.ReceiverUserId == userId && friendShip.FriendShipStatus == 3
            select new FriendShipsListModel
            {
                FriendId = users.UserId,
                Name = users.Name,
                FriendPhotoPath = users.ProfileImagePath ?? string.Empty
            }).ToListAsync(ct);

        if (sentRequestFriends.Count <= 0)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        model.AddFriendRequestList = sentRequestFriends;
        model.Response.Set(ResponseConstants.S0000);
        return model;
    }

    public async Task<AddFriendSentResponseModel> AddFriendSentRequest(string userId, AddFriendSentRequestModel request,
        CancellationToken ct)
    {
        AddFriendSentResponseModel model = new();

        if (request.FriendId.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }
        
        var isExist = await _context.Users
            .AnyAsync(x => x.UserId == request.FriendId && !x.IsDeleted, ct);
        
        if (!isExist)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }
        
        if (request.FriendId.Equals(userId))
        {
            model.Response.Set(ResponseConstants.W0008);
            return model;
        }

        var isFriend = await _context.Friends
            .AnyAsync(x => x.UserIdOne == userId && x.UserIdTwo == request.FriendId, ct);

        if (isFriend)
        {
            model.Response.Set(ResponseConstants.W0007);
            return model;
        }

        var isSentFriend = await _context.FriendShips
            .AnyAsync(x => x.SenderUserId == userId && x.ReceiverUserId == request.FriendId && x.FriendShipStatus == 3,
                ct);
        if (isSentFriend)
        {
            model.Response.Set(ResponseConstants.W0006);
            return model;
        }

        Friendships data = new()
        {
            SenderUserId = userId,
            ReceiverUserId = request.FriendId,
            FriendShipStatus = (int)EnumFriendShipsType.Pending,
            SendDate = DateTime.Now,
        };

        await _context.FriendShips.AddAsync(data, ct);
        var result = await _context.SaveChangesAsync(ct);
        if (result <= 0)
        {
            model.Response.Set(ResponseConstants.E0000);
            return model;
        }

        model.AddFriendSentRequestStatus = EnumFriendShipsType.Pending.ToString();
        model.Response.Set(ResponseConstants.S0000);
        return model;
    }

    public async Task<ApproveFriendSentResponseModel> ApproveFriendSentRequest(string userId,
        ApproveFriendSentRequestModel request, CancellationToken ct)
    {
        ApproveFriendSentResponseModel model = new();

        if (request.FriendId.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }
        
        var friendShip = await _context.FriendShips
            .FirstOrDefaultAsync(x => x.ReceiverUserId == userId && x.SenderUserId == request.FriendId, ct);

        if (friendShip is null)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            friendShip.FriendShipStatus =
                request.IsAccept ? (int)EnumFriendShipsType.Accepted : (int)EnumFriendShipsType.Rejected;
            friendShip.ApprovedDate = DateTime.Now;
            _context.FriendShips.Update(friendShip);
            var result = await _context.SaveChangesAsync(ct);
            if (result <= 0)
            {
                model.Response.Set(ResponseConstants.E0000);
                return model;
            }

            if (request.IsAccept)
            {
                Friend friends = new()
                {
                    UserIdOne = userId,
                    UserIdTwo = request.FriendId,
                    CreatedDate = DateTime.Now
                };
                await _context.Friends.AddAsync(friends, ct);
                var friendOneResult = await _context.SaveChangesAsync(ct);
                if (friendOneResult <= 0)
                {
                    model.Response.Set(ResponseConstants.E0000);
                    return model;
                }

                var friendDate = friends.CreatedDate;
                friends = new Friend
                {
                    UserIdOne = request.FriendId,
                    UserIdTwo = userId,
                    CreatedDate = friendDate
                };
                await _context.Friends.AddAsync(friends, ct);
                var friendTwoResult = await _context.SaveChangesAsync(ct);
                if (friendTwoResult <= 0)
                {
                    model.Response.Set(ResponseConstants.E0000);
                    return model;
                }
            }

            await transaction.CommitAsync(ct);
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

    public async Task<UnFriendResponseModel> UnFriend(string userId, UnFriendRequestModel request, CancellationToken ct)
    {
        UnFriendResponseModel model = new();
        if (request.FriendId.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }

        var friendShipOne = await _context.Friends
            .FirstOrDefaultAsync(x => x.UserIdOne == userId && x.UserIdTwo == request.FriendId && !x.IsUnfriend, ct);
        if (friendShipOne is null)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }


        friendShipOne.IsUnfriend = true;
        _context.Friends.Update(friendShipOne);
        var friendOneResult = await _context.SaveChangesAsync(ct);
        if (friendOneResult <= 0)
        {
            model.Response.Set(ResponseConstants.E0000);
            return model;
        }

        var friendShipTwo = await _context.Friends
            .FirstOrDefaultAsync(x => x.UserIdTwo == userId && x.UserIdOne == request.FriendId && !x.IsUnfriend, ct);
        if (friendShipTwo is null)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        friendShipTwo.IsUnfriend = true;
        _context.Friends.Update(friendShipOne);
        var result = await _context.SaveChangesAsync(ct);
        var response = result > 0 ? ResponseConstants.S0000 : ResponseConstants.E0000;
        model.Response.Set(response);
        return model;
    }
}