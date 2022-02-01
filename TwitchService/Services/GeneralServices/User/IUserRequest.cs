using TwitchService.Data.ObjectResponse;

namespace TwitchService.Services.GeneralServices.User;

public interface IUserRequest
{
    Task<ObjectStreamerInfo> GetInfoAsync(string token, string user);
    Task<ObjectStreamerOn> GetStreamAsync(string token, string user);
}