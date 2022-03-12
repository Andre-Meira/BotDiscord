using System;
using System.Collections.Generic;

namespace DataBaseApplication.Models
{
    public partial class RelStreamerXDiscordserf
    {
        public RelStreamerXDiscordserf(){}

        public RelStreamerXDiscordserf(int fkStreamer, long fkIdServ)
        {
            FkStreamer = fkStreamer;
            FkIdServe = fkIdServ;
        }

        public int IdRel { get; set; }
        public int FkStreamer { get; set; }
        public long FkIdServe { get; set; }

        public virtual Discordserver FkIdServeNavigation { get; set; } = null!;
        public virtual Streamerdisc FkStreamerNavigation { get; set; } = null!;
    }
}
