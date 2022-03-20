using TwitchService.Data.ObjectResponse;

public interface IDiscordService
{    
    Task SendMsgStreamOn(ObjectStreamerInfo objectStreamerInfo, ObjectStreamerOn objectStreamer, long idChannelDiscord);      
} 