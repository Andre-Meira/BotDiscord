using System;
using System.Collections.Generic;

namespace DataBaseApplication.Models
{
    public partial class RelStreamerXDiscordserf
    {
        public int IdRel { get; set; }
        public int FkStreamer { get; set; }
        public long FkIdServe { get; set; }

        public virtual Discordserver FkIdServeNavigation { get; set; } = null!;
        public virtual Streamerdisc FkStreamerNavigation { get; set; } = null!;
    }
}
