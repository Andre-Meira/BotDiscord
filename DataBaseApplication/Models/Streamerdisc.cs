using System;
using System.Collections.Generic;

namespace DataBaseApplication.Models
{
    public partial class Streamerdisc
    {
        public Streamerdisc()
        {
            RelStreamerXDiscordserves = new HashSet<RelStreamerXDiscordserf>();
        }

        public int IdStreamer { get; set; }
        public string Nickname { get; set; } = null!;

        public virtual ICollection<RelStreamerXDiscordserf> RelStreamerXDiscordserves { get; set; }
    }
}
