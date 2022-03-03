using System;
using System.Collections.Generic;

namespace DataBaseApplication.Models
{
    public partial class Discordserver
    {
        public Discordserver(){}

        public Discordserver(long idServer, long idchannel, string? nameServe = null,
            string? nameChannel = null)
        {
            IdServer = idServer;
            IdChanel = idchannel;
            NameServer = nameServe;
            NameChannel = nameChannel;

            RelStreamerXDiscordserves = new HashSet<RelStreamerXDiscordserf>();            
        }        

        public long IdServer { get; set; }
        public long IdChanel { get; set; }
        public string? NameServer { get; set; }
        public string? NameChannel { get; set; }

        public virtual ICollection<RelStreamerXDiscordserf> RelStreamerXDiscordserves { get; set; }
    }
}
